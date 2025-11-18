using JetBrains.Annotations;
using UnityEngine;

namespace Objetos
{
    public class Parametrizador : MonoBehaviour
    {
        public string localidade = "null";
        public RoomType roomType = RoomType.Unknown;
        [CanBeNull] public string campus;
        public UserType userType = UserType.Undefined;
        
        public void SetLocalidade(string localidade)
        {
            this.localidade  = localidade;
        }

        public void SetRoomType(string roomType)
        {
            RoomType.TryParse(roomType, out this.roomType);
        }

        public void SetCampus(string campus)
        {
            this.campus = campus;
        }
    }
    
    
}