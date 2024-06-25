using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AlimentosMvc.Models;
using AlimentosMvc.Controllers;

namespace AlimentosMvc.Controllers
{
    public class UsuariosController : Controller
    {
        public string UriBase = "torresssgio.somee.com/Usuarios/";


        [HttpGet]
        public ActionResult Index()
            {
                return View("CadastrarUsuario");
            }
   [HttpPost]
        public async Task<ActionResult> RegistrarAsync(UsuarioViewModel u)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient()) 
                {
                    string uriComplementar = "Registrar";

                    var content = new StringContent(JsonConvert.SerializeObject(u));
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(UriBase + uriComplementar, content);

                    string serialized = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["Mensagem"] = "Usuario registrado com sucesso!";
                        return View("AutenticarUsuario");
                    }
                    else
                    {
                        throw new Exception(serialized);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

                [HttpPost]
        public async Task<ActionResult> AutenticarAsync(UsuarioViewModel u)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string uriComplementar = "Autenticar";

                    var content = new StringContent(JsonConvert.SerializeObject(u));
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(UriBase + uriComplementar, content);

                    string serialized = await response.Content.ReadAsStringAsync();

                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        UsuarioViewModel uLogado = JsonConvert.DeserializeObject<UsuarioViewModel>(serialized);
                        HttpContext.Session.SetString("SessionTokenUsuario", uLogado.Token);
                        return RedirectToAction("Index", "Personagem");
                    }
                    else 
                    {
                        throw new Exception(serialized);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return null;
            }
        }

        [HttpGet]
        public async Task<ActionResult> IndexInformacoesAsync()
        {
        try
        {
        HttpClient httpClient = new HttpClient();
        //Novo: Recuperação informação da sessão
        string login = HttpContext.Session.GetString("SessionUsername");
        string uriComplementar =
        $"GetByLogin/{login}";
        HttpResponseMessage response = await httpClient.GetAsync(UriBase +
        uriComplementar);
        string serialized = await response.Content.ReadAsStringAsync();
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
        UsuarioViewModel u = await Task.Run(() =>
        JsonConvert.DeserializeObject<UsuarioViewModel>(serialized));
        return View(u);
        }
        else
        {
        throw new System.Exception(serialized);
        }
        }
        catch (System.Exception ex)
        {
        TempData["MensagemErro"] = ex.Message;
        return RedirectToAction("Index");
        }
        }
[HttpGet]
public async Task<ActionResult> ObterDadosAlteracaoSenha()
{
UsuarioViewModel viewModel = new UsuarioViewModel();
try
{
HttpClient httpClient = new HttpClient();
string login = HttpContext.Session.GetString("SessionUsername");
string uriComplementar = $"GetByLogin/{login}";
HttpResponseMessage response = await httpClient.GetAsync(UriBase +
uriComplementar);
string serialized = await response.Content.ReadAsStringAsync();
TempData["TituloModalExterno"] = "Alteração de Senha";
if (response.StatusCode == System.Net.HttpStatusCode.OK)
{
viewModel = await Task.Run(() =>
JsonConvert.DeserializeObject<UsuarioViewModel>(serialized));
return PartialView("_AlteracaoSenha", viewModel);
}
else
throw new System.Exception(serialized);
}
catch (System.Exception ex)
{
TempData["MensagemErro"] = ex.Message;
return RedirectToAction("IndexInformacoes");
}                
}

}
}

