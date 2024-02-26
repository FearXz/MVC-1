using MVC_1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            List<Dipendente> dipendenti = new List<Dipendente>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Dipendenti";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dipendente dipendente = new Dipendente();
                    dipendente.IDDipendente = Convert.ToInt32(reader["IDDipendente"]);
                    dipendente.Nome = reader["Nome"].ToString();
                    dipendente.Cognome = reader["Cognome"].ToString();
                    dipendente.Indirizzo = reader["Indirizzo"].ToString();
                    dipendente.CodiceFiscale = reader["CodiceFiscale"].ToString();
                    dipendente.Mansione = reader["Mansione"].ToString();
                    dipendente.Coniugato = Convert.ToBoolean(reader["Coniugato"]);
                    dipendente.NFigliACarico = Convert.ToInt32(reader["NFigliACarico"]);

                    dipendenti.Add(dipendente);

                }

            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante il recupero dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return View(dipendenti);
        }

        public ActionResult CreateDipendente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDipendente(Dipendente dipendente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query =
                    "INSERT INTO Dipendenti (Nome,Cognome,Indirizzo,CodiceFiscale,Mansione,Coniugato,NFigliACarico) " +
                    "VALUES (@Nome,@Cognome,@Indirizzo,@CodiceFiscale,@Mansione,@Coniugato,@NFigliACarico) ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", dipendente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", dipendente.Cognome);
                cmd.Parameters.AddWithValue("@Indirizzo", dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@CodiceFiscale", dipendente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@Mansione", dipendente.Mansione);
                cmd.Parameters.AddWithValue("@Coniugato", dipendente.Coniugato);
                cmd.Parameters.AddWithValue("@NFigliACarico", dipendente.NFigliACarico);

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante il recupero dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        public ActionResult CreatePagamenti()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePagamenti(Pagamento p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query =
                    "INSERT INTO Pagamenti (IDDipendente,DataPagamento,Ammontare,Tipo) " +
                    "VALUES (@IDDipendente,@DataPagamento,@Ammontare,@Tipo) ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDDipendente", p.IDDipendente);
                cmd.Parameters.AddWithValue("@DataPagamento", p.DataPagamento);
                cmd.Parameters.AddWithValue("@Ammontare", p.Ammontare);
                cmd.Parameters.AddWithValue("@Tipo", p.Tipo);

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante il recupero dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return View();
        }

        public ActionResult Pagamenti()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            List<Pagamento> pagamenti = new List<Pagamento>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Pagamenti";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Pagamento pagamento = new Pagamento();
                    pagamento.IDPagamento = Convert.ToInt32(reader["IDPagamento"]);
                    pagamento.IDDipendente = Convert.ToInt32(reader["IDDipendente"]);
                    pagamento.DataPagamento = Convert.ToDateTime(reader["DataPagamento"]);
                    pagamento.Ammontare = Convert.ToDecimal(reader["Ammontare"]);
                    pagamento.Tipo = reader["Tipo"].ToString();

                    pagamenti.Add(pagamento);

                }
            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante il recupero dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return View(pagamenti);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}