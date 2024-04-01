using AutoMapper;
using Flurl.Http;
using PagSeguro.DotNet.Sdk.Common.Providers;
using PagSeguro.DotNet.Sdk.Common.Settings;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Charges;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders;
using PagSeguro.DotNet.Sdk.Orders.Dtos.Orders.ChargedOrder;
using PagSeguro.DotNet.Sdk.Orders.Helpers;
using PagSeguro.DotNet.Sdk.Orders.Interfaces.Orders;

namespace PagSeguro.DotNet.Sdk.Orders.Providers.Orders
{
    public abstract class ChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto> : BaseProvider,
        IChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto>
        where TChargeWriteDto : ChargeDto
        where TChargeReadDto : ChargeDto
    {
        private ChargedOrderWriteDto<TChargeWriteDto> _chargedOrderWriteDto;
        private readonly IMapper _mapper;

        public ChargedOrderProviderOf(
            PagSeguroSettings settings,
            IMapper mapper)
            : base(settings)
        {
            InitOrder();
            _mapper = mapper;
        }

        private void InitOrder()
        {
            _chargedOrderWriteDto = new ChargedOrderWriteDto<TChargeWriteDto>();
        }

        public IChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto> AddCharge(TChargeWriteDto chargeWrite)
        {
            _chargedOrderWriteDto.Charges.Add(chargeWrite);
            return this;
        }

        public IChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto> AddCharges(
            ICollection<TChargeWriteDto> chargeWriteDtos)
        {
            List<TChargeWriteDto> newCharges = _chargedOrderWriteDto.Charges.ToList();
            newCharges.AddRange(chargeWriteDtos);
            _chargedOrderWriteDto.Charges = newCharges;
            return this;
        }

        public IChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto> Load(
            ChargedOrderWriteDto<TChargeWriteDto> chargedWriteDto)
        {
            _chargedOrderWriteDto = chargedWriteDto;
            return this;
        }

        public IChargedOrderProviderOf<TChargeWriteDto, TChargeReadDto> Load(OrderWriteDto orderWriteDto)
        {
            _chargedOrderWriteDto = _mapper.Map<ChargedOrderWriteDto<TChargeWriteDto>>(orderWriteDto);
            return this;
        }

        public ChargedOrderWriteDto<TChargeWriteDto> Build()
        {
            ChargedOrderWriteDto<TChargeWriteDto> order = _chargedOrderWriteDto;
            InitOrder();
            return order;
        }

        public async Task<ChargedOrderReadDto<TChargeReadDto>> CreateAsync()
        {
            var orderReadDto = await BaseUrl
                .AppendPathSegment(OrderEndpoint.Orders)
                .WithOAuthBearerToken(Settings.Token)
                .WithHeader(OrderHeaders.IdempotencyKey, Guid.NewGuid())
                .PostJsonAsync(_chargedOrderWriteDto)
                .ReceiveJson<ChargedOrderReadDto<TChargeReadDto>>();
            InitOrder();
            return orderReadDto;
        }

        public async Task<ChargedOrderReadDto<TChargeReadDto>> GetByIdAsync(string orderId)
        {
            return await BaseUrl
                .AppendPathSegment(OrderEndpoint.Orders)
                .AppendPathSegment(orderId)
                .WithOAuthBearerToken(Settings.Token)
                .GetJsonAsync<ChargedOrderReadDto<TChargeReadDto>>();
        }

        public async Task<ChargedOrderReadDto<TChargeReadDto>> PayAsync(string orderId)
        {
            var orderReadtDto = await BaseUrl
                .AppendPathSegments(OrderEndpoint.Orders, orderId, OrderEndpoint.Pay)
                .WithOAuthBearerToken(Settings.Token)
                .PostJsonAsync(new
                {
                    charges = _chargedOrderWriteDto.Charges
                })
                .ReceiveJson<ChargedOrderReadDto<TChargeReadDto>>();
            InitOrder();
            return orderReadtDto;
        }
    }
}
