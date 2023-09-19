using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Sincronizacion_Hilos
{
    class Program
    {
        static void Main(string[] args)
        {
            CuentaBancaria CuentaFamilia = new CuentaBancaria(20000);

            Thread[] hilosPersonas = new Thread[15];

            for (int i = 0; i < 15; i++)
            {
                Thread t = new Thread(CuentaFamilia.VamosRetirarEfectivo);

                t.Name = i.ToString();

                hilosPersonas[i] = t;
                hilosPersonas[i].Start();
                hilosPersonas[i].Join();
            }            

            Console.ReadKey();
        }
    }

    class CuentaBancaria
    {        

        double Saldo { set; get; }

        public CuentaBancaria(double Saldo)
        {
              this.Saldo = Saldo;
        }

        public double RetirarEfectivo(double cantidad)
        {
            if ((Saldo - cantidad) < 0)
                {
                    Console.WriteLine($"Lo siento queda {Saldo} en la cuenta. Hilo: {Thread.CurrentThread.Name}");
                    return Saldo;
                }

            
            if (Saldo >= cantidad)
            {
                Console.WriteLine("Retirado {0} y queda {1} en la cuenta {2}", cantidad, (Saldo - cantidad), Thread.CurrentThread.Name);
                Saldo -= cantidad;
            }

            return Saldo;
            
        }
        public void VamosRetirarEfectivo()
        {
            Console.WriteLine("Esta sacando dinero el hilo: {0}", Thread.CurrentThread.Name);

            for (int i = 0; i < 3; i++)
            {
                RetirarEfectivo(500);
            }
        }
    }
}
