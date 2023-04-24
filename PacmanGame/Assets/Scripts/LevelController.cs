using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OttiPostLewis.Lab6 {
    public class LevelController : MonoBehaviour
    {
        [Header("Cameras")]
        [SerializeField] Camera isometricCamera, topDownCamera, mixedCamera;
        [SerializeField] Vector2 pacmanSpawnStoryMode;
        [SerializeField] List<Vector2> infiniteModeSpawnPoints;
        private List<Camera> cameras = new List<Camera>();
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Level Controller");
            // disable all cameras and enable only the selected one
            cameras.Add(isometricCamera);
            cameras.Add(topDownCamera);
            cameras.Add(mixedCamera);
            foreach(Camera camera in cameras) {
                camera.enabled = false;
            }
            switch(GameManager.selectedCameraMode) {
                case (int) GameManager.CameraModes.Isometric:
                    isometricCamera.enabled = true;
                    break;
                case (int) GameManager.CameraModes.TopDown:
                    topDownCamera.enabled = true;
                    break;
                case (int) GameManager.CameraModes.Mixed:
                    mixedCamera.enabled = true;
                    break;
            }
            Vector2 pacmanLoc = GetStartingPacmanLocation();
            GameObject pacman = GameObject.Find("PacMan");
            pacman.transform.position = new Vector3(pacmanLoc.x, pacman.transform.position.y, pacmanLoc.y);
        }
        public Vector2 GetStartingPacmanLocation() {
            if(GameManager.selectedGameMode == (int)GameManager.Gamemodes.Story) {
                return pacmanSpawnStoryMode;
            }
            else {
                return infiniteModeSpawnPoints[Random.Range(0, infiniteModeSpawnPoints.Count)];
            }
        }
    }
}