using System;

namespace API.Models
{
    public class Folha
    {
        public Folha(){
            this.CriadoEm = DateTime.Now;
        }
        public int Id { get; set; }
        public string CpfFuncionario { get; set; }
        public int HorasTrabalhadas { get; set; }
        public double ValorHora { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public double SalarioBruto { get; set; }
        public double ImpostoRenda{ get; set; }
        public double Inss{ get; set; }
        public double Fgts{ get; set; }
        public double salarioLiquido{ get; set; }
        public DateTime CriadoEm { get; set; }
    }
}