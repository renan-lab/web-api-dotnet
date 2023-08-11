using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repositories.Database.SQLServer.ADO
{
    public class Medico : IRepository<Models.Medico>
    {
        private readonly SqlConnection conn;
        private readonly SqlCommand cmd;
        private readonly string chaveCache;

        public Medico(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            chaveCache = "medicos";
        }

        public List<Models.Medico> get()
        {
            List<Models.Medico> medicos = (List<Models.Medico>)Cache.get(chaveCache);

            if (medicos != null)
                return medicos;

            medicos = new List<Models.Medico>();
          
            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select codigo, nome, datanasc, crm from medico";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Models.Medico medico = new Models.Medico();
                            medico.Codigo = (int)dr["codigo"];
                            medico.Nome = (string)dr["nome"];
                            if (dr["datanasc"] != DBNull.Value)
                                medico.DataNasc = (DateTime)dr["datanasc"];
                            else
                                medico.DataNasc = null;
                            medico.CRM = (string)dr["crm"];

                            medicos.Add(medico);
                        }
                    }
                }
            }

            Cache.add(chaveCache, medicos);

            return medicos;
        }

        public Models.Medico getById(int id)
        {
            List<Models.Medico> medicos = (List<Models.Medico>)Cache.get(chaveCache);

            if (medicos != null)
                return medicos.Find(medicoCache => medicoCache.Codigo == id);

            Models.Medico medico = null;

            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select codigo, nome, datanasc, crm from medico where codigo = @codigo;";
                    cmd.Parameters.Add(new SqlParameter("@codigo", System.Data.SqlDbType.Int)).Value = id;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            medico = new Models.Medico();
                            medico.Codigo = (int)dr["codigo"];
                            medico.Nome = (string)dr["nome"];
                            if (dr["datanasc"] != DBNull.Value)
                                medico.DataNasc = (DateTime)dr["datanasc"];
                            else
                                medico.DataNasc = null;
                            medico.CRM = (string)dr["crm"];
                        }
                    }
                }
            }

            return medico;
        }

        public void add(Models.Medico medico)
        {
            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into medico(nome, datanasc, crm) values(@nome, @datanasc, @crm); select convert(int,@@identity) as codigo;";

                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = medico.Nome;
                    if (medico.DataNasc != null)
                        cmd.Parameters.Add(new SqlParameter("@datanasc", System.Data.SqlDbType.Date)).Value = medico.DataNasc;
                    else
                        cmd.Parameters.Add(new SqlParameter("@datanasc", System.Data.SqlDbType.Date)).Value = DBNull.Value;
                    cmd.Parameters.Add(new SqlParameter("@crm", System.Data.SqlDbType.Char)).Value = medico.CRM;

                    medico.Codigo = (int)cmd.ExecuteScalar();
                }
            }

            Cache.delete(chaveCache);
        }

        public int update(int id, Models.Medico medico)
        {
            int linhasAfetadas = 0;
            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "update medico set nome = @nome, datanasc = @datanasc, crm = @crm where codigo = @codigo;";

                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = medico.Nome;
                    if (medico.DataNasc != null)
                        cmd.Parameters.Add(new SqlParameter("@datanasc", System.Data.SqlDbType.Date)).Value = medico.DataNasc;
                    else
                        cmd.Parameters.Add(new SqlParameter("@datanasc", System.Data.SqlDbType.Date)).Value = DBNull.Value;
                    cmd.Parameters.Add(new SqlParameter("@crm", System.Data.SqlDbType.Char)).Value = medico.CRM;
                    cmd.Parameters.Add(new SqlParameter("@codigo", System.Data.SqlDbType.Int)).Value = id;

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }

            Cache.delete(chaveCache);

            return linhasAfetadas;
        }

        public int delete(int id)
        {
            int linhasAfetadas = 0;
            using (conn)
            {
                conn.Open();

                using (cmd)
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "delete from medico where codigo = @codigo;";
                    cmd.Parameters.Add(new SqlParameter("@codigo", System.Data.SqlDbType.Int)).Value = id;

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
            }

            Cache.delete(chaveCache);

            return linhasAfetadas;
        }
    }
}
