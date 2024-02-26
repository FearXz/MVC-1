using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using MVC_1.Models;

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
                    "INSERT INTO Dipendenti (Nome,Cognome,Indirizzo,CodiceFiscale,Mansione,Coniugato,NFigliACarico) "
                    + "VALUES (@Nome,@Cognome,@Indirizzo,@CodiceFiscale,@Mansione,@Coniugato,@NFigliACarico) ";

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
                    "INSERT INTO Pagamenti (IDDipendente,DataPagamento,Ammontare,Tipo) "
                    + "VALUES (@IDDipendente,@DataPagamento,@Ammontare,@Tipo) ";

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

            return RedirectToAction("Pagamenti");
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

        public ActionResult DeleteDipendente(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "DELETE FROM Dipendenti WHERE IDDipendente = @IDDipendente";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDDipendente", id);

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

        public ActionResult EditDipendente(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            Dipendente dipendente = new Dipendente();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Dipendenti WHERE IDDipendente = @IDDipendente";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDDipendente", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dipendente.IDDipendente = Convert.ToInt32(reader["IDDipendente"]);
                    dipendente.Nome = reader["Nome"].ToString();
                    dipendente.Cognome = reader["Cognome"].ToString();
                    dipendente.Indirizzo = reader["Indirizzo"].ToString();
                    dipendente.CodiceFiscale = reader["CodiceFiscale"].ToString();
                    dipendente.Mansione = reader["Mansione"].ToString();
                    dipendente.Coniugato = Convert.ToBoolean(reader["Coniugato"]);
                    dipendente.NFigliACarico = Convert.ToInt32(reader["NFigliACarico"]);
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

            return View(dipendente);
        }

        [HttpPost]
        public ActionResult EditDipendente(Dipendente d)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query =
                    "UPDATE Dipendenti SET Nome = @Nome, Cognome = @Cognome, Indirizzo = @Indirizzo, CodiceFiscale = @CodiceFiscale, Mansione = @Mansione, Coniugato = @Coniugato, NFigliACarico = @NFigliACarico WHERE IDDipendente = @IDDipendente";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDDipendente", d.IDDipendente);
                cmd.Parameters.AddWithValue("@Nome", d.Nome);
                cmd.Parameters.AddWithValue("@Cognome", d.Cognome);
                cmd.Parameters.AddWithValue("@Indirizzo", d.Indirizzo);
                cmd.Parameters.AddWithValue("@CodiceFiscale", d.CodiceFiscale);
                cmd.Parameters.AddWithValue("@Mansione", d.Mansione);
                cmd.Parameters.AddWithValue("@Coniugato", d.Coniugato);
                cmd.Parameters.AddWithValue("@NFigliACarico", d.NFigliACarico);

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

        public ActionResult DetailDipendente(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            List<DetailDipendente> listaDipendenti = new List<DetailDipendente>();

            try
            {
                conn.Open();

                string query =
                    "SELECT d.IDDipendente, d.Nome,d.Cognome,p.DataPagamento,p.Ammontare,p.Tipo FROM Dipendenti AS d INNER JOIN Pagamenti AS p ON d.IDDipendente = p.IDDipendente WHERE d.IDDipendente = @IDDipendente";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDDipendente", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DetailDipendente dipendente = new DetailDipendente();
                    dipendente.IDDipendente = Convert.ToInt32(reader["IDDipendente"]);
                    dipendente.Nome = reader["Nome"].ToString();
                    dipendente.Cognome = reader["Cognome"].ToString();
                    dipendente.DataPagamento = Convert.ToDateTime(reader["DataPagamento"]);
                    dipendente.Ammontare = Convert.ToDecimal(reader["Ammontare"]);
                    dipendente.Tipo = reader["Tipo"].ToString();

                    listaDipendenti.Add(dipendente);
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

            return View(listaDipendenti);
        }
    }
}
