using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Library
{
   public class LPaginador<T>
   {
      private int CantidadPaginador = 50;
      private int CantidadEnlacesNav = 3;
      private int PaginaActual;
      private string PaginaAnterior = "&laquo; Anterior";
      private string PaginaSiguiente = "Siguiente &raquo; ";
      private string PrimeraPagina = "&laquo; Primero ";
      private string UltimaPagina = "Ultimo &raquo; ";
      private string PaginaNavegacion = null;
      public object[] paginador(List<T> tabla, int pagina, int registros,
         string area, string controller, string action, string host)
      {
         PaginaActual = pagina == 0 ? 1 : pagina;
         CantidadPaginador = registros > 0 ? registros : CantidadPaginador;
         int TotalRegistros = tabla.Count;
         double valor1 = Math.Ceiling((double)TotalRegistros / (double)CantidadPaginador);
         int TotalPaginasPaginador = Convert.ToInt16(Math.Ceiling(valor1));
         if (PaginaActual != 1)
         {
            int PaginaUrl = 1;
            PaginaNavegacion += "<a class='btn btn-default' href='" 
               + host + "/" + controller + "/" + action + "?id="
               + PaginaUrl + "&registros=" + CantidadPaginador + "&area=" + area + "'>" + PrimeraPagina + "</a>";

            PaginaUrl = PaginaActual - 1;
            PaginaNavegacion += "<a class='btn btn-default' href='"
               + host + "/" + controller + "/" + action + "?id="
               + PaginaUrl+ "&registros="+ CantidadPaginador + "&area=" + area + "'>" + PaginaAnterior + "</a>";
         }
         double valor2 = (CantidadEnlacesNav / 2);
         int paginaIntervalo = Convert.ToInt16(Math.Round(valor2));
         int paginaDesde = PaginaActual - paginaIntervalo;
         int paginaHasta = PaginaActual + paginaIntervalo;
         if (paginaDesde < 1)
         {
            paginaHasta -= (paginaDesde - 1);
            paginaDesde = 1;
         }
         if (paginaHasta>TotalPaginasPaginador)
         {
            paginaDesde -= (paginaHasta - TotalPaginasPaginador);
            paginaHasta = TotalPaginasPaginador;
            if (paginaDesde<1)
            {
               paginaDesde = 1;
            }
         }
         for (int pagi = paginaDesde; pagi <= paginaHasta; pagi++)
         {
            if (pagi == PaginaActual)
            {
               PaginaNavegacion += "<span class='btn btn-default' disabled='disabled'>" + pagi + "</span>";
            }
            else
            {
               PaginaNavegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" +
                  action + "?id=" + pagi + "&registros=" + CantidadPaginador + "&area='" + area + "'>" +
                  pagi + "</a>";
            }
         }
         if (PaginaActual < TotalPaginasPaginador)
         {
            int paginaUrl = PaginaActual + 1;
            PaginaNavegacion += "<a class='btn btn-default' href'"+ host + "/" + controller + "/" +
                   action + "?id=" + paginaUrl + "&registros=" + CantidadPaginador + "&area='" + area + "'>" +
                   PaginaSiguiente + "</a>";
            paginaUrl = TotalPaginasPaginador;
            PaginaNavegacion += "<a class='btn btn-default' href'" + host + "/" + controller + "/" +
                   action + "?id=" + paginaUrl + "&registros=" + CantidadPaginador + "&area='" + area + "'>" +
                   UltimaPagina + "</a>";
         }
         return null;
      }
   }
}
