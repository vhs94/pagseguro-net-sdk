using Xunit;

// Os testes de integracao batem na API real do PagBank com uma unica conta de
// sandbox compartilhada. Rodar as classes em paralelo estoura o rate limit da API
// (HTTP 429) e faz os testes falharem em conjunto mesmo passando isolados.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
