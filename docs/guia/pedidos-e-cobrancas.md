# Pedidos e cobranças

No PagBank um **pedido** (`/orders`) agrupa cliente, itens e entrega. Uma
**cobrança** (`/charges`) é o dinheiro em si. Dá para criar os dois juntos ou
cobrar avulso.

## Cobrança avulsa com cartão de crédito

```csharp
ChargeByCreditCardResponse cobranca = await client
    .ForCharge()
    .WithCreditCard()
    .AddPaymentMethod(new CreditCardPaymentMethodRequest
    {
        Installments = 1,
        Capture = true,
        SoftDescriptor = "MINHALOJA",
        Card = new CardRequest
        {
            Number = "4539620659922097",
            ExpMonth = 12,
            ExpYear = 2030,
            SecurityCode = 123,
            Holder = new Holder { Name = "Jose da Silva" }
        }
    })
    .WithAmount(new ChargeAmountRequest { Value = 1000, Currency = "BRL" })
    .WithReferenceId("pedido-00001")
    .WithDescription("Motivo do pagamento")
    .ChargeAsync();
```

### Pré-autorizar e capturar depois

Envie `Capture = false` para apenas reservar o valor e capture quando quiser:

```csharp
ChargeByCreditCardResponse capturada = await client
    .ForCharge()
    .WithCreditCard()
    .WithId(cobranca.Id!)
    .CaptureAsync(1000);
```

### Cancelar ou estornar

```csharp
await client.ForCharge().WithCreditCard().WithId(cobranca.Id!).CancelAsync(1000);
```

O valor é em centavos e aceita estorno parcial.

## Pedido com cobrança em uma chamada

```csharp
ChargeByCreditCardRequest cobrancaRequest = client
    .ForCharge()
    .WithCreditCard()
    .AddPaymentMethod(meioDePagamento)
    .WithAmount(new ChargeAmountRequest { Value = 1000, Currency = "BRL" })
    .WithDescription("Motivo do pagamento")
    .Build();

ChargedOrderResponse<ChargeByCreditCardResponse> pedido = await client
    .ForOrder()
    .WithReferenceId("pedido-00001")
    .WithCustomer(new Customer
    {
        Name = "Jose da Silva",
        Email = "jose@exemplo.com",
        TaxId = "12345678909"
    })
    .WithItem(new ItemRequest
    {
        ReferenceId = "item-00001",
        Name = "Produto",
        Quantity = 1,
        UnitAmount = 1000
    })
    .WithCreditCard()
    .AddCharge(cobrancaRequest)
    .CreateAsync();
```

## Pix por QR Code

No PagBank o Pix é pedido pelos **QR Codes do pedido**, não por um meio de
pagamento da cobrança:

```csharp
OrderResponse pedido = await client
    .ForOrder()
    .WithReferenceId("pedido-pix-00001")
    .WithCustomer(cliente)
    .WithItem(item)
    .WithQrCode(new QrCodeRequest
    {
        Amount = new QrCodeAmount { Value = 1000 }
    })
    .CreateAsync();

string copiaECola = pedido.QrCodes.First().Text!;
string imagemPng = pedido.QrCodes.First().Links
    .First(link => link.Rel == "QRCODE.PNG").Href!;
```

## Boleto

```csharp
ChargeByBankSlipResponse boleto = await client
    .ForCharge()
    .WithBankSlip()
    .AddBankSlip(new BankSlipRequest
    {
        DueDate = DateTime.Today.AddDays(3),
        InstructionLines = new InstructionLines
        {
            FirstLine = "Pagamento processado para DESCRICAO",
            SecondLine = "Via PagBank"
        },
        Holder = new BankSlipHolder
        {
            Name = "Jose da Silva",
            TaxId = "12345678909",
            Email = "jose@exemplo.com",
            Address = endereco
        }
    })
    .WithAmount(new ChargeAmountRequest { Value = 1000, Currency = "BRL" })
    .ChargeAsync();

string linhaDigitavel = boleto.PaymentMethod!.BankSlip!.FormattedBarCode!;
```

## Cartão de débito com 3DS

A autenticação 3DS é **obrigatória** para débito:

```csharp
ChargeByDebitCardWith3DsAuthResponse cobranca = await client
    .ForCharge()
    .WithDebitCardAnd3DsAuthentication()
    .AddPaymentMethod(new DebitCardWith3DsAuthPaymentMethodRequest
    {
        Card = cartao,
        AuthenticationMethod = new AuthenticationMethodRequest
        {
            Type = "THREEDS",
            Cavv = "...",
            Eci = "05",
            Version = "2.1.0"
        }
    })
    .WithAmount(new ChargeAmountRequest { Value = 1000, Currency = "BRL" })
    .ChargeAsync();
```

## Consultando

```csharp
OrderResponse pedido = await client.ForOrder().GetByIdAsync("ORDE_...");

// pedidos que contêm uma cobrança específica
ICollection<OrderResponse> pedidos = await client
    .ForOrder()
    .GetByChargeIdAsync("CHAR_...");
```

## Simulando taxas

```csharp
FeeResponse taxas = await client
    .ForFee()
    .WithValue(10000)
    .WithMaxInstallments(12)
    .WithMaxInstallmentsNoInterest(3)
    .CalculateAsync();
```
