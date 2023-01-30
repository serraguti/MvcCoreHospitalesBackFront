using MvcCoreHospitalesBackFront.Models;
using System.Net.Http.Headers;

namespace MvcCoreHospitalesBackFront.Services
{
    public class ServiceApiHospital
    {
        private string UrlApi;

        //TENDREMOS UN OBJETO PARA INDICAR EL TIPO DE PETICION
        //QUE HAREMOS (JSON)
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiHospital()
        {
            //LAS URL DE APIS EN LOS SERVICES SOLAMENTE SE INCLUYE
            //EL SITIO WEB, NO LA PETICION (NO api/hospitales)
            this.UrlApi = "https://apihospitalesazure2023pgs.azurewebsites.net/";
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        //TAMBIEN PODEMOS TENER UN CONSTRUCTOR DONDE INDICAMOS QUE 
        //EL CONTAINER PROGRAM NOS INDIQUE LA URL
        public ServiceApiHospital(string urlapi)
        {
            this.UrlApi = urlapi;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        //LOS METODOS DE CONSUMO DE APIS SON ASINCRONOS
        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            //UTILIZAMOS EL OBJETO HttpClient PARA LA PETICION
            using (HttpClient client = new HttpClient())
            {
                //LA PETICION
                string request = "/api/hospitales";
                //AÑADIMOS AL CLIENTE LA DIRECCION BASE DE LA URL DEL API
                client.BaseAddress = new Uri(this.UrlApi);
                //LIMPIAMOS LA CABECERA DE OTRAS PETICIONES
                client.DefaultRequestHeaders.Clear();
                //AÑADIMOS AL HEADER EL TIPO DE DATOS QUE VAMOS A CONSUMIR
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //REALIZAMOS UNA PETICION ASINCRONA CON EL METODO GET
                //Y NOS DEVOLVERA UNA RESPUESTA DE TIPO HttpResponseMessage
                HttpResponseMessage response = await client.GetAsync(request);
                //COMPROBAMOS SI LA RESPUESTA ES CORRECTA
                if (response.IsSuccessStatusCode)
                {
                    //AQUI TENEMOS LOS DATOS
                    //EN LA PROPIEDAD Content DE LA RESPUESTA VIENEN LOS 
                    //DATOS EN FORMATO JSON
                    //DEBEMOS CONVERTIR LOS DATOS A CLASES MEDIANTE UN
                    //METODO LLAMADO ReadAsAsync()
                    List<Hospital> hospitales =
                        await response.Content.ReadAsAsync<List<Hospital>>();
                    return hospitales;
                }
                else
                {
                    //ALGO HA FALLADO
                    //SIEMPRE QUE ALGO FALLA, DEVOLVEREMOS NULL DESDE EL SERVICIO
                    return null;
                }
            }
        }
    }
}
