using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Libreria para trabajar con BD
using System.Data;
using System.Data.SqlClient; //Libreria para conectarse una BD de SQL Server

namespace AvilesJenifer
{

        class Class1
        {
            SqlConnection miConexion = new SqlConnection();
            SqlCommand comandosSQL = new SqlCommand();
            SqlDataAdapter miAdaptadorDatos = new SqlDataAdapter();

            DataSet ds = new DataSet();

            public Class1()
            {
                String cadena = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_parqueo_vehicular_aviles_jenifer.mdf;Integrated Security=True";
                miConexion.ConnectionString = cadena;
                miConexion.Open();
            }


            public DataSet obtener_datos()
            {
                ds.Clear();
                comandosSQL.Connection = miConexion;

                comandosSQL.CommandText = "select * from tbl_vehiculo";
                miAdaptadorDatos.SelectCommand = comandosSQL;
                miAdaptadorDatos.Fill(ds, "tbl_vehiculo");

                return ds;
            }

            public void mantenimiento_datos(String[] datos, String accion)
            {
            String sql = "";
            if (accion == "nuevo")
            {

                sql = "INSERT INTO tbl_vehiculo (marca, modelo, year) VALUES(" +

                    "'" + datos[1] + "'," +
                    "'" + datos[2] + "'," +
                    "'" + datos[3] + "'" +
                    ")";

            }

            else if (accion == "modificar")
            {

                sql = "UPDATE tbl_vehiculo SET " +
                " marca              = '" + datos[1] + "'," +
                " modelo           = '" + datos[2] + "'," +
                " year            = '" + datos[3] + "'" +
                " WHERE IdVehiculo     = '" + datos[0] + "'";


            }
            else if (accion == "eliminar")
            {
                sql = "DELETE tbl_vehiculo FROM tbl_vehiculo WHERE IdVehiculo='" + datos[0] + "'";

            }
            procesarSQL(sql);
        }

        void procesarSQL(String sql)
            {
                comandosSQL.Connection = miConexion;
                comandosSQL.CommandText = sql;
                comandosSQL.ExecuteNonQuery();
            }

        }
    }



