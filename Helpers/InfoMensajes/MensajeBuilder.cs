using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.InfoMensajes
{
    public static class MensajeBuilder
    {
        #region mensajes para adicionar
        public static string CrearMsgSuccess(string entity) { return string.Format("{0} creado", entity); }
        public static string CrearMsgExist(string entity) { return string.Format("{0} ya existe", entity); }
        public static string CrearMsgError(string entity) { return string.Format("Error creando {0}", entity); }

        public static string MsgExisting(string entity) { return string.Format("{0} ya existe en el sistema", entity); }
        #endregion

        #region mensajes para editar
        public static string EditarMsgSuccess(string entity) { return string.Format("{0} editado", entity); }
        public static string EditarMsgExist(string entity) { return string.Format("{0} no editable", entity); }
        public static string EditarMsgError(string entity) { return string.Format("Error editando {0}", entity); }
        #endregion

        #region mensajes para borrar
        public static string BorrarMsgSuccess(string entity) { return string.Format("{0} borrado", entity); }
        public static string BorrarMsgExist(string entity) { return string.Format("{0} no se puede borrar", entity); }
        public static string BorrarMsgError(string entity) { return string.Format("Error borrando {0}", entity); }
        #endregion

        #region Mensajes genericos
        public static string MsgErrorData(string entity) { return string.Format("Datos inconsistentes en {0}", entity); }
        #endregion
    }
}
