namespace EinRedMesh.API.Models
{
    public class RespuestaModel
    {
        public bool HayError { get; set; }
        public string Mensaje { get; set; }
        public Object Data { get; set; }

        public int Codigo { get; set; }

        public RespuestaModel(string mensajeError, object data=null)
        {
            HayError = true;
            Mensaje = mensajeError;
            Codigo = 500;
            Data = data;
        }

        public RespuestaModel(int statusCode, string Mensaje, object data=null)
        {
            HayError = statusCode<= 299 ? false : true;
            this.Mensaje = Mensaje;
            Codigo = statusCode;
            Data = data;
            
        }



    }
}
