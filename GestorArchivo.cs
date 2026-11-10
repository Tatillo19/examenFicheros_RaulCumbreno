using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class GestorArchivo {
    // METODO 1:
    // Método para leer el fichero CSV y cargarlo en una lista
    public static List<Partido> LeerCsv(string ruta) {
        List<Partido> partidos = new List<Partido>();
        // Leemos todas las líneas del fichero CSV
        string[] lineas = File.ReadAllLines(ruta); 
        
        // Empezamos en 1 para saltarnos la cabecera
        for (int i = 1; i < lineas.Length; i++) {
            // Separamos la línea por comas
            string[] campos = lineas[i].Split(','); 
            
            // Si la línea tiene 9 campos, creamos el objeto y lo añadimos a la lista
            if (campos.Length == 9) {
                Partido p = new Partido(campos[0], campos[1], campos[2], campos[3], campos[4], 
                                        campos[5], campos[6], campos[7], campos[8]);
                partidos.Add(p);
            }
        }
        return partidos;
    }

    // METODO 2:
    // Método para buscar y mostrar los resultados de un equipo en concreto
    public static void ProcesarEquipo(List<Partido> partidos, string equipo, string rutaSalida) {
        StringBuilder textoFichero = new StringBuilder(); 
        textoFichero.AppendLine("RESULTADOS DE " + equipo.ToUpper());
        
        // Recorremos los partidos y filtramos si el equipo juega como local o visitante
        foreach (Partido p in partidos) {
            if (p.GetEquipoLocal() == equipo || p.GetEquipoVisitante() == equipo) {
                textoFichero.AppendLine(p.ToString());
            }
        }
        
        // Printeamos los resultados por consola
        string contenido = textoFichero.ToString();
        Console.WriteLine(contenido);

        // Guardamos en fichero si el usuario ha introducido un nombre
        GuardarSiHayRuta(contenido, rutaSalida);
    }

    // METODO 3:
    // Método para buscar y mostrar los resultados de una jornada en concreto
    public static void ProcesarJornada(List<Partido> partidos, string jornada, string rutaSalida) {
        StringBuilder textoFichero = new StringBuilder();
        textoFichero.AppendLine("RESULTADOS DE " + jornada.ToUpper());
        
        // Recorremos los partidos y filtramos por el nombre de la jornada
        foreach (Partido p in partidos) {
            if (p.GetJornada() == jornada) {
                textoFichero.AppendLine(p.ToString());
            }
        }
        
        // Printeamos los resultados por consola
        string contenido = textoFichero.ToString();
        Console.WriteLine(contenido);

        // Guardamos en fichero si el usuario ha introducido un nombre
        GuardarSiHayRuta(contenido, rutaSalida);
    }

    // METODO 4:
    // Método para calcular y mostrar el Top 3 de los jugadores con más MVPs
    public static void ProcesarTopMvps(List<Partido> partidos, string rutaSalida) {
        // 1. Sacamos la lista de nombres de jugadores MVP sin repetir
        List<string> nombres = new List<string>();
        foreach (Partido p in partidos) {
            if (!nombres.Contains(p.GetMvpPartido())) {
                nombres.Add(p.GetMvpPartido());
            }
        }

        // 2. Contamos cuántas veces aparece cada jugador como MVP
        List<int> conteo = new List<int>();
        for (int i = 0; i < nombres.Count; i++) {
            int veces = 0;
            foreach (Partido p in partidos) {
                if (p.GetMvpPartido() == nombres[i]) {
                    veces++;
                }
            }
            conteo.Add(veces);
        }

        // 3. Ordenamos de mayor a menOR
        for (int i = 0; i < conteo.Count; i++) {
            for (int j = 0; j < conteo.Count - 1; j++) {
                if (conteo[j] < conteo[j + 1]) {
                    int tempNum = conteo[j]; 
                    conteo[j] = conteo[j + 1]; 
                    conteo[j + 1] = tempNum;
                    
                    string tempNom = nombres[j]; 
                    nombres[j] = nombres[j + 1]; 
                    nombres[j + 1] = tempNom;
                }
            }
        }

        // 4. Preparamos el texto con los 3 primeros jugadores de la lista
        StringBuilder textoFichero = new StringBuilder();
        textoFichero.AppendLine("TOP 3 MVPs");
        for (int i = 0; i < 3 && i < nombres.Count; i++) {
            textoFichero.AppendLine((i + 1) + ". " + nombres[i] + " - " + conteo[i] + " veces");
        }

        // Printeamos por consola
        string contenido = textoFichero.ToString();
        Console.WriteLine(contenido);

        // Guardamos en fichero si el usuario ha introducido un nombre
        GuardarSiHayRuta(contenido, rutaSalida);
    }

    // METODO 5: 
    // Método para calcular y mostrar la clasificación global por victorias y derrotas
    public static void ProcesarClasificacion(List<Partido> partidos, string rutaSalida) {
        // 1. Obtenemos la lista con todos los equipos sin repetir
        List<string> equipos = new List<string>();
        foreach (Partido p in partidos) {
            if (!equipos.Contains(p.GetEquipoLocal())){
                equipos.Add(p.GetEquipoLocal());
            }
            if (!equipos.Contains(p.GetEquipoVisitante())){
                equipos.Add(p.GetEquipoVisitante());
            }
        }

        // 2. Inicializamos los contadores de victorias y derrotas a cero
        List<int> victorias = new List<int>();
        List<int> derrotas = new List<int>();
        for (int i = 0; i < equipos.Count; i++) {
            victorias.Add(0);
            derrotas.Add(0);
        }

        // 3. Recorremos los partidos para calcular los puntos y asignar victorias y derrotas
        foreach (Partido p in partidos) {
            int posLocal = 0;
            int posVisita = 0;

            // Buscamos la posición del equipo local y visitante en la lista
            for (int i = 0; i < equipos.Count; i++) {
                if (equipos[i] == p.GetEquipoLocal()){
                    posLocal = i;
                }
                if (equipos[i] == p.GetEquipoVisitante()){
                    posVisita = i;
                }
            }

            try {
                // Separamos el resultado por el guion para comparar los puntos
                string[] puntos = p.GetResultado().Split('-');
                int ptsLocal = int.Parse(puntos[0]);
                int ptsVisita = int.Parse(puntos[1]);

                if (ptsLocal > ptsVisita) {
                    victorias[posLocal]++;
                    derrotas[posVisita]++;
                } else if (ptsVisita > ptsLocal) {
                    victorias[posVisita]++;
                    derrotas[posLocal]++;
                }
            } 
            catch (Exception ex) {
                Console.WriteLine("Error al calcular el resultado de " + p.GetEquipoLocal() + " vs " + p.GetEquipoVisitante() + ": " + ex.Message);
            }
        }

        // 4. Ordenamos la clasificación por victorias
        for (int i = 0; i < victorias.Count; i++) {
            for (int j = 0; j < victorias.Count - 1; j++) {
                if (victorias[j] < victorias[j + 1]) {
                    int tVic = victorias[j];
                    victorias[j] = victorias[j + 1]; 
                    victorias[j + 1] = tVic;

                    int tDer = derrotas[j]; 
                    derrotas[j] = derrotas[j + 1]; 
                    derrotas[j + 1] = tDer;

                    string tEq = equipos[j]; 
                    equipos[j] = equipos[j + 1]; 
                    equipos[j + 1] = tEq;
                }
            }
        }

        // 5. Formateamos la clasificación para mostrar en pantalla y guardar
        StringBuilder textoFichero = new StringBuilder();
        textoFichero.AppendLine("CLASIFICACIÓN ACB");
        for (int i = 0; i < equipos.Count; i++) {
            textoFichero.AppendLine((i + 1) + ". " + equipos[i] + " | Victorias: " + victorias[i] + " | Derrotas: " + derrotas[i]);
        }

        // Printeamos por consola
        string contenido = textoFichero.ToString();
        Console.WriteLine(contenido);

        // Guardamos en fichero si el usuario ha introducido un nombre
        GuardarSiHayRuta(contenido, rutaSalida);
    }

    // Método para generar un fichero .txt por cada equipo de la liga
    public static void GenerarFicherosPorEquipo(List<Partido> partidos) {
        // Sacamos la lista de equipos únicos
        List<string> equipos = new List<string>();
        foreach (Partido p in partidos) {
            if (!equipos.Contains(p.GetEquipoLocal())) {
                equipos.Add(p.GetEquipoLocal());
            }
        }

        // Para cada equipo, recopilamos sus partidos y creamos su archivo .txt
        foreach (string equipo in equipos) {
            StringBuilder textoFichero = new StringBuilder();
            textoFichero.AppendLine("Resultados de la temporada de " + equipo);
            
            foreach (Partido p in partidos) {
                if (p.GetEquipoLocal() == equipo || p.GetEquipoVisitante() == equipo) {
                    textoFichero.AppendLine(p.ToString());
                }
            }
            
            try {
                // Reemplazamos espacios por guiones bajos para crear un nombre de archivo limpio
                string nombreArchivo = "Data/" + equipo.Replace(" ", "_") + ".txt";
                File.WriteAllText(nombreArchivo, textoFichero.ToString()); 
            }
            catch (Exception ex) {
                Console.WriteLine("Error al crear el fichero de " + equipo + ": " + ex.Message);
            }
        }
    }

    // METODO 6:
    // Método auxiliar para escribir el contenido en un archivo si la ruta elegida por el usuario no está vacía
    private static void GuardarSiHayRuta(string contenido, string rutaSalida) {
        if (rutaSalida != "") {
            try {
                File.WriteAllText("Data/" + rutaSalida, contenido); 
                Console.WriteLine("Datos guardados correctamente en Data/" + rutaSalida);
            }
            catch (Exception ex) {
                Console.WriteLine("Error al guardar el archivo: " + ex.Message);
            }
        }
    }
}