using LojaAPI.Domain.Exceptions;
using LojaAPI.Domain.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace LojaAPI.Infra.CrossCutting
{
    public static class Validations
    {
        public static async Task ValidateInputs(Cliente cliente)
        {
            await ValidateCpfCnpj(cliente.cd_CPF, cliente.cd_CNPJ);

            if (!String.IsNullOrWhiteSpace(cliente.cd_CPF)) await ValidateCpf(cliente.cd_CPF);

            if (!String.IsNullOrWhiteSpace(cliente.cd_CNPJ)) await ValidateCnpj(cliente.cd_CNPJ);

            await ValidateCep(cliente);

            await ValidateEmail(cliente.ds_Email);

            await ValidateTelefone(cliente.telefonesCliente);

            await ValidateClassificacao(cliente.ds_Classificacao);
        }

        public static async Task<bool> ValidateCpfCnpj(string clienteCPF, string clienteCNPJ)
        {
            if (String.IsNullOrWhiteSpace(clienteCPF) && String.IsNullOrWhiteSpace(clienteCNPJ)) throw new InputValidationException("Preencha ao menos um dos campos a seguir: CPJ, CNPJ.");

            if (!String.IsNullOrWhiteSpace(clienteCPF) && !String.IsNullOrWhiteSpace(clienteCNPJ)) throw new InputValidationException("Preencha somente um dos campos a seguir: CPJ, CNPJ.");

            return true;
        }

        public static async Task<bool> ValidateCpf(string clienteCPF)
        {
            string[] cpfsInvalidos = new string[] { "00000000000", "11111111111", "22222222222", "33333333333", "44444444444", "55555555555", "66666666666", "77777777777", "88888888888", "99999999999" };

            if (!cpfsInvalidos.Contains(clienteCPF)) 
            {
                int cont = 10;
                int aux = 0;
                int i;

                if (long.TryParse(clienteCPF, out long _))
                {
                    for (i = 0; i <= 8; i++)
                    {
                        aux += int.Parse(clienteCPF[i].ToString()) * cont;
                        cont--;
                    }

                    aux *= 10;
                    aux %= 11;

                    if (aux == 10) aux = 0;

                    if (aux == int.Parse(clienteCPF[9].ToString())) 
                    {
                        cont = 11;
                        aux = 0;

                        for (i = 0; i <= 9; i++)
                        {
                            aux += int.Parse(clienteCPF[i].ToString()) * cont;
                            cont--;
                        }

                        aux *= 10;
                        aux %= 11;

                        if (aux == 10) aux = 0;
                        if (aux == int.Parse(clienteCPF[10].ToString())) 
                        {
                            return true;    
                        }
                    }
                }
            }
            throw new InputValidationException("CPF inválido.");
        }

        public static async Task<bool> ValidateCnpj(string clienteCNPJ)
        {
            int[] pesos1 = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] pesos2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int aux = 0;
            int i;

            if (long.TryParse(clienteCNPJ, out long _)) 
            {
                for (i = 0; i <= 11; i++)
                {
                    aux += int.Parse(clienteCNPJ[i].ToString()) * pesos1[i];
                }

                aux %= 11;

                if (aux < 2) aux = 0;
                else aux = 11 - aux;

                if (aux == int.Parse(clienteCNPJ[12].ToString())) 
                {
                    aux = 0;

                    for (i = 0; i <= 12; i++)
                    {
                        aux += int.Parse(clienteCNPJ[i].ToString()) * pesos2[i];
                    }

                    aux %= 11;

                    if (aux < 2) aux = 0;
                    else aux = 11 - aux;

                    if (aux == int.Parse(clienteCNPJ[13].ToString()))
                    {
                        return true;
                    }
                }
            }
            throw new InputValidationException("CNPJ inválido.");
        }

        public static async Task<bool> ValidateCep(Cliente cliente)
        {
            string clienteCEP = cliente.cd_CEP;

            if (!String.IsNullOrWhiteSpace(clienteCEP))
            {
                if (int.TryParse(clienteCEP, out int _)) 
                {
                    HttpWebRequest request =  (HttpWebRequest)WebRequest.Create($"https://viacep.com.br/ws/{clienteCEP}/json/");
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode != HttpStatusCode.OK) throw new ServiceUnavailableException("Servidor VIACEP indisponível.");

                    using (Stream webStream = response.GetResponseStream())
                    {
                        if (webStream != null)
                        {
                            using (StreamReader responseReader = new StreamReader(webStream))
                            {
                                string strResponse = responseReader.ReadToEnd();
                                dynamic CEP = JsonConvert.DeserializeObject<object>(strResponse);

                                if (CEP.erro != "true") 
                                {
                                    cliente.nm_Logradouro = CEP.logradouro;
                                    cliente.ds_Complemento = CEP.complemento;
                                    cliente.nm_Bairro = CEP.bairro;
                                    cliente.nm_Cidade = CEP.localidade;
                                    cliente.cd_Estado = CEP.uf;

                                    return true;
                                }
                            }
                        }
                    }
                }
                throw new InputValidationException("CEP inválido.");
            }
            return true;
        }

        public static async Task<bool> ValidateEmail(string emailCliente)
        {
            try
            {
                MailAddress m = new MailAddress(emailCliente);
                return true;
            }
            catch (FormatException)
            {
                throw new InputValidationException("Email inválido.");
            }
        }

        public static async Task<bool> ValidateTelefone(List<TelefoneCliente> telefonesCliente)
        {
            if (telefonesCliente != null && telefonesCliente.Any())
            {
                Regex rgx = new Regex(@"^\d{2}9?\d{8}$");

                foreach (var telefoneCliente in telefonesCliente)
                {
                    if (!rgx.IsMatch(telefoneCliente.cd_Telefone)) throw new InputValidationException($"Número de telefone: {telefoneCliente.cd_Telefone} inválido.");
                }
            }
            else throw new InputValidationException("Insira ao menos um número de telefone.");

            return true;
        }

        public static async Task<bool> ValidateClassificacao(string clienteClassificacao)
        {
            string[] aStrClassificacao = new string[] { "Ativo", "Inativo", "Preferencial" };
            if (!aStrClassificacao.Contains(clienteClassificacao)) throw new InputValidationException("Classificação inválida.");
            return true;

        }
    }
}
