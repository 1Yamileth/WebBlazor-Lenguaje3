namespace Blazor_Vista
{
    public class Config
    {
        public string  CadenaConexion { get; set; }//ESTA NOS LEE LA CADENA DE CONEXION DE NUESTRO APPSETTING
       //Le pasamos la cadena mediante el constructor
        public Config(string _cadenaconexion)
        {
            CadenaConexion = _cadenaconexion;
        }
    }
}
