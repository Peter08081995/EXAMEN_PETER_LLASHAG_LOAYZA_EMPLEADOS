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

        public Employee employee { get; set; }


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
            employee = new Employee();
        }

        static void Main(string[] args)
        {
            var Program = new Program();
            int valor = 0, valor2 = 0, valor3 = 0;
            bool estado, esTextoNombre = true;
            string valorIngresado, valorSalario, valorEdad;
            bool esNumero, esNumeroSalario, esNumeroEdad;

            string nombre = string.Empty, rutaImagen = string.Empty;

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
                        Console.WriteLine("Ingrese una opción válida, y presiona Enter");
                        valorIngresado = Console.ReadLine();
                        esNumero = int.TryParse(valorIngresado, out valor);
                    }
                    while (!esNumero);

                    if (valor == 2)
                    {

                        do
                        {
                            Console.WriteLine("Ingrese nombre del Empleado válido");
                            nombre = Console.ReadLine();
                            if (Program.IsLetters(nombre))
                            {
                                esTextoNombre = false;
                            }
                        } while (esTextoNombre);

                        do
                        {
                            Console.WriteLine("Ingrese salario del Empleado válido");
                            valorSalario = Console.ReadLine();
                            esNumeroSalario = int.TryParse(valorSalario, out valor2);
                        } while (!esNumeroSalario);

                        do
                        {
                            Console.WriteLine("Ingrese edad del Empleado válido");
                            valorEdad = Console.ReadLine();
                            esNumeroEdad = int.TryParse(valorEdad, out valor3);
                        } while (!esNumeroEdad);

                        Console.WriteLine("Ingresé ruta imagen del Empleado válido");
                        rutaImagen = Console.ReadLine();

                        Program.employee.Employee_name = nombre;
                        Program.employee.Employee_age = int.Parse(valorEdad);
                        Program.employee.Employee_salary = int.Parse(valorSalario);
                        Program.employee.Profile_image = rutaImagen;

                    }

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

                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------\n");
                        Console.WriteLine(tarea.Result.ToString() + "\n");
                        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------");
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Ocurrio algún error." + ex);
                    }

                    break;
                case 2:

                    try
                    {
                        Task<String> tarea2 = Task.Run(async () => await Program.GuardarEmployee(Program.employee, Program.UrlEmployee));
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------\n");
                        Console.WriteLine(tarea2.Result.ToString() + "\n");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
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

        private bool IsLetters(string caracteres)
        {
            foreach (char ch in caracteres)
            {
                if (!Char.IsLetter(ch) && ch != 32)
                {
                    return false;
                }
            }
            return true;
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
