using System;
using System.Collections.Generic;

string rutaCsv = "Data/acb2526.csv";
List<Partido> partidos = new List<Partido>();

try {
    partidos = GestorArchivo.LeerCsv(rutaCsv);
}
catch (Exception ex) {
    Console.WriteLine("Error al leer el archivo: " + ex.Message);
    return; 
}

int opcion = 0;

while (opcion != 6) {
    Console.WriteLine();
    Console.WriteLine("GESTIÓN LIGA ACB 25-26");
    Console.WriteLine("1. Ver resultados de un equipo");
    Console.WriteLine("2. Ver resultados de una jornada");
    Console.WriteLine("3. Ver Top 3 jugadores MVPs");
    Console.WriteLine("4. Ver la clasificación de equipos");
    Console.WriteLine("5. Exportar ficheros por equipo");
    Console.WriteLine("6. Salir");
    Console.WriteLine("Elige una opción: ");

    try {
        opcion = int.Parse(Console.ReadLine());
    }
    catch {
        Console.WriteLine("Error: Introduce un número válido.");
        opcion = 0;
    }

    switch (opcion) {
        case 1:
            Console.WriteLine("Introduce el nombre del equipo: ");
            string equipo = Console.ReadLine();
            Console.WriteLine("Nombre del txt para guardar: ");
            string rutaEquipo = Console.ReadLine();
            Console.WriteLine();
            GestorArchivo.ProcesarEquipo(partidos, equipo, rutaEquipo);
            break;

        case 2:
            Console.WriteLine("Introduce el nombre de la jornada: ");
            string jornada = Console.ReadLine();
            Console.WriteLine("Nombre del txt para guardar: ");
            string rutaJornada = Console.ReadLine();
            Console.WriteLine();
            GestorArchivo.ProcesarJornada(partidos, jornada, rutaJornada);
            break;

        case 3:
            Console.WriteLine("Nombre del txt para guardar: ");
            string rutaMvp = Console.ReadLine();
            Console.WriteLine();
            GestorArchivo.ProcesarTopMvps(partidos, rutaMvp);
            break;

        case 4:
            Console.WriteLine("Nombre del txt para guardar: ");
            string rutaClasif = Console.ReadLine();
            Console.WriteLine();
            GestorArchivo.ProcesarClasificacion(partidos, rutaClasif);
            break;

        case 5:
            Console.WriteLine();
            try {
                GestorArchivo.GenerarFicherosPorEquipo(partidos);
                Console.WriteLine("Ficheros de equipos generados en la carpeta Data.");
            } catch (Exception ex) {
                Console.WriteLine("Hubo un error: " + ex.Message);
            }
            break;

        case 6:
            Console.WriteLine("Cerrando el programa. Agur!!");
            break;

        default:
            if (opcion != 0) Console.WriteLine("NO VALE ESTA OPCIÓN!");
            break;
    }
}