﻿using MiniProyectoBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProyectoBanking.Core.Application.ViewModels.Transacciones
{
    public class TransaccionViewModel
    {
        public List<TransaccionViewModel> Transacciones { get; set; }
        public int Id { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaOrigenId { get; set; }
        public Producto CuentaOrigen { get; set; }
        public int CuentaDestinoId { get; set; }
        public Producto CuentaDestino { get; set; }
    }
}
