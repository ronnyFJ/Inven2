//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inven2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Articulos
    {
        public int IdArticulos { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> Existencia { get; set; }
        public Nullable<int> Id_Tipo_Inve { get; set; }
        public Nullable<decimal> Costo_unitario { get; set; }
        public Nullable<bool> Estado { get; set; }
        public Nullable<int> Id_Asiento_cont { get; set; }
    
        public virtual Tipo_Inventario Tipo_Inventario { get; set; }
    }
}
