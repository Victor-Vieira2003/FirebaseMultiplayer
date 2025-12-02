using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    #region Metodos

        public void RE_LoadScene()
        {
            var cenaatual = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            UnityEngine.SceneManagement.SceneManager.LoadScene(cenaatual.name);
        }
    #endregion
}
