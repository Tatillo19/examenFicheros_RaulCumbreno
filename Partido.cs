public class Partido {
    // Atributos
    private string jornada;
    private string equipoLocal;
    private string equipoVisitante;
    private string resultado;
    private string mvpPartido;
    private string puntosMvp;
    private string asistenciasMvp;
    private string rebotesMvp;
    private string equipoMvp;

    // Constructor
    public Partido(string pJor, string pEqLoc, string pEqVis, string pRes, string pMvp, 
                   string pPts, string pAsis, string pReb, string pEqMvp) {
        jornada = pJor;
        equipoLocal = pEqLoc;
        equipoVisitante = pEqVis;
        resultado = pRes;
        mvpPartido = pMvp;
        puntosMvp = pPts;
        asistenciasMvp = pAsis;
        rebotesMvp = pReb;
        equipoMvp = pEqMvp;
    }

    // Getters y Setters tradicionales
    public string GetJornada() {
        return jornada;
    }
    public void SetJornada(string value) {
        jornada = value;
    }

    public string GetEquipoLocal() {
        return equipoLocal;
    }
    public void SetEquipoLocal(string value) {
        equipoLocal = value;
    }

    public string GetEquipoVisitante() {
        return equipoVisitante;
    }
    public void SetEquipoVisitante(string value) {
        equipoVisitante = value;
    }

    public string GetResultado() {
        return resultado;
    }
    public void SetResultado(string value) {
        resultado = value;
    }

    public string GetMvpPartido() {
        return mvpPartido;
    }
    public void SetMvpPartido(string value) {
        mvpPartido = value;
    }

    public string GetPuntosMvp() {
        return puntosMvp;
    }
    public void SetPuntosMvp(string value) {
        puntosMvp = value;
    }

    public string GetAsistenciasMvp() {
        return asistenciasMvp;
    }
    public void SetAsistenciasMvp(string value) {
        asistenciasMvp = value;
    }

    public string GetRebotesMvp() {
        return rebotesMvp;
    }
    public void SetRebotesMvp(string value) {
        rebotesMvp = value;
    }

    public string GetEquipoMvp() {
        return equipoMvp;
    }
    public void SetEquipoMvp(string value) {
        equipoMvp = value;
    }

    // Método ToString para mostrar la información del partido
    public override string ToString() {
        return $"{jornada}: {equipoLocal} vs {equipoVisitante} | Resultado: {resultado} | MVP: {mvpPartido} ({puntosMvp} pts)";
    }
}