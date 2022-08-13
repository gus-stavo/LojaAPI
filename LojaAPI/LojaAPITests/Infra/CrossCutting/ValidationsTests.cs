using System.Collections;
using LojaAPI.Domain.Models;
using LojaAPI.Infra.CrossCutting;

namespace LojaAPITests.Infra.CrossCutting
{
    public class ValidationsTests
    {
        public class ValidaCepParameters : IEnumerable<Object[]> 
        {
            public IEnumerator<object[]> GetEnumerator() 
            {
                yield return new object[]
                {
                    new Cliente
                    {
                        cd_CEP = ""
                    }
                };

                yield return new object[]
                {
                    new Cliente
                    {
                        cd_CEP = "e"
                    }
                };

                yield return new object[]
                {
                    new Cliente
                    {
                        cd_CEP = "1146301e"
                    }
                };

                yield return new object[]
                {
                    new Cliente
                    {
                        cd_CEP = "11111111"
                    }
                };

                yield return new object[]
                {
                    new Cliente
                    {
                        cd_CEP = "11463010"
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ValidaTelefoneParameters : IEnumerable<Object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new List<TelefoneCliente>()
                    {
                        
                    }
                };

                yield return new object[]
                {
                    new List<TelefoneCliente>()
                    {
                        new TelefoneCliente{ cd_Telefone = "13974224510" },
                        new TelefoneCliente{ cd_Telefone = "1112345678" },
                    }
                };

                yield return new object[]
                {
                    new List<TelefoneCliente>()
                    {
                        new TelefoneCliente{ cd_Telefone = "13974224510" },
                        new TelefoneCliente{ cd_Telefone = "55" },
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("44937742825", "12345678911234")]
        [InlineData("44937742825", "")]
        [InlineData("", "11111111111111")]
        public async Task ValidateCpfCnpj(string cpfCliente, string cnpjCliente)
        {
            bool retorno = await Validations.ValidateCpfCnpj(cpfCliente, cnpjCliente);

            Assert.True(retorno);
        }

        [Theory]
        [InlineData("")]
        [InlineData("e")]
        [InlineData("4493774282e")]
        [InlineData("44937742825")]
        [InlineData("44937742826")]
        public async Task ValidateCpf(string cpfCliente)
        {
            bool retorno = await Validations.ValidateCpf(cpfCliente);

            Assert.True(retorno);
        }

        [Theory]
        [InlineData("")]
        [InlineData("e")]
        [InlineData("0276212100141e")]
        [InlineData("02762121001410")]
        [InlineData("11111111111111")]
        public async Task ValidateCnpj(string cnpjCliente)
        {
            bool retorno = await Validations.ValidateCnpj(cnpjCliente);

            Assert.True(retorno);
        }

        [Theory]
        [ClassData(typeof(ValidaCepParameters))]
        public async Task ValidateCep(Cliente cliente)
        {
            bool retorno = await Validations.ValidateCep(cliente);

            Assert.True(retorno);
        }

        [Theory]
        [InlineData("")]
        [InlineData("g")]
        [InlineData("g@g@g")]
        [InlineData("g@g")]
        public async Task ValidateEmail(string emailCliente)
        {
            bool retorno = await Validations.ValidateEmail(emailCliente);

            Assert.True(retorno);
        }

        [Theory]
        [ClassData(typeof(ValidaTelefoneParameters))]
        public async Task ValidateTelefone(List<TelefoneCliente> telefonesCliente)
        {
            bool retorno = await Validations.ValidateTelefone(telefonesCliente);

            Assert.True(retorno);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Ativo")]
        [InlineData("Inativo")]
        [InlineData("Preferencial")]
        [InlineData("Prefeencial")]
        public async Task ValidateClassificacao(string classificacaoCliente)
        {
            bool retorno = await Validations.ValidateClassificacao(classificacaoCliente);

            Assert.True(retorno);
        }
    }
}
