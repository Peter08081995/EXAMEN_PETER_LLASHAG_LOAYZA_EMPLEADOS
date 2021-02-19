using Newtonsoft.Json;
using SX.ERP.Entidad.Atributtes;
using SX.ERP.Entidad.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Empleado
{
    class Program
    {
        public int MaxResponseContentBufferSize { get; set; }
        public TimeSpan Timeout { get; set; }
        public HttpResponseMessage Respuesta { get; private set; }
        public string UrlDummy { get; set; }
        public string UrlEmployee { get; set; }

        private string _resultado = string.Empty;

        public string Resultado
        {
            get { return _resultado; }
            set { _resultado = value; }
        }

        public Program()
        {
            MaxResponseContentBufferSize = 2147483647;
            Timeout = new TimeSpan(0, 0, 30);
            UrlDummy = "http://dummy.restapiexample.com/api/v1/employees";
            UrlEmployee = "http://localhost:8086/employee";
        }

        static void Main(string[] args)
        {
            var Program = new Program();
            int valor = 0;
            bool estado;
            string valorIngresado;
            bool esNumero;

            Console.WriteLine("Consumiendo api rest\r");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Opciones:\n");
            Console.WriteLine("1.-Obtener datos Dummy\r");
            Console.WriteLine("2.-Registrar empleado\r");
            Console.WriteLine("------------------------\n");

            try
            {

                do
                {
                    do
                    {
                        Console.WriteLine("Ingresa una opción válida, y presiona Enter");
                        valorIngresado = Console.ReadLine();
                        esNumero = int.TryParse(valorIngresado, out valor);
                    }
                    while (!esNumero);

                    if (valor == 1 || valor == 2)
                        estado = false;
                    else
                        estado = true;

                } while (estado);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error: " + ex);
            }


            switch (valor)
            {
                case 1:

                    try
                    {
                        Task<String> tarea = Task.Run(async () => await Program.ObtenerDatosDummy(Program.UrlDummy));
                        var jsonResult = tarea.Result;


                        var data = JsonConvert.DeserializeObject<Listado<Employee>>(jsonResult);
                        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("Id\t" + "Nombre\t\t\t\t\t" + "Salario\t\t\t" + "Edad\t\t\t" + "Ruta Imagen");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------\n");
                        foreach (Employee employee in data.Data)
                        {
                            Console.WriteLine(employee.Id + "\t" + employee.Employee_name + "\t\t\t\t" + employee.Employee_salary + "\t\t\t" + employee.Employee_age + "\t\t" + employee.Profile_image);
                        }
                        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------\n");
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Ocurrio algún error." + ex);
                    }

                    break;
                case 2:

                    try
                    {
                        Task<String> tarea2 = Task.Run(async () => await Program.GuardarEmployee(null, Program.UrlEmployee));
                        Console.WriteLine(tarea2.Result.ToString());
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Ocurrio algún error." + ex);
                    }
                    break;

            }

            Console.Write("Presiona la tecla close para salir de la aplicación de consola...");
            Console.ReadKey();
        }

        private async Task<String> ObtenerDatosDummy(string url)
        {
            Stream receiveStream = new MemoryStream();
            HttpClient cliente = new HttpClient();

            cliente.MaxResponseContentBufferSize = MaxResponseContentBufferSize;
            cliente.Timeout = Timeout;

            Respuesta = cliente.GetAsync(url).Result;

            receiveStream = await Respuesta.Content.ReadAsStreamAsync();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            Resultado = readStream.ReadToEnd();

            return Resultado;
        }

        private async Task<String> GuardarEmployee(Employee employee, string url)
        {
            employee = new Employee();

            employee.Employee_age = 28;
            employee.Employee_name = "Peter";
            employee.Employee_salary = 3000;
            employee.Profile_image = "";

            Stream receiveStream = new MemoryStream();
            HttpClient cliente = new HttpClient();
            cliente.MaxResponseContentBufferSize = MaxResponseContentBufferSize;
            cliente.Timeout = Timeout;
            Respuesta = cliente.GetAsync(url).Result;

            var json = JsonConvert.SerializeObject(employee);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            Respuesta = cliente.PostAsync(url, content).Result;

            receiveStream = await Respuesta.Content.ReadAsStreamAsync();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            Resultado = readStream.ReadToEnd();

            return Resultado;
        }
    }
}
