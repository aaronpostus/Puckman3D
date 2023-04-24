using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OttiPostLewis.Lab6 {
    public class LevelController : MonoBehaviour
    {
        [Header("Cameras")]
        [SerializeField] Camera isometricCamera, topDownCamera, mixedCamera;
        [SerializeField] public List<Ghost> ghosts;
        [SerializeField] public Vector2 pacmanSpawnStoryMode;
        [SerializeField] public List<Vector2> infiniteModeSpawnPoints;
        private List<Camera> cameras = new List<Camera>();
        private GameManager gm;
        private Transform pelletTransform;
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
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.PauseLevel();
            ResetGhostAndPacmanPositions();
            GatherPelletsInLevel();
        }
        void GatherPelletsInLevel() {
            this.pelletTransform = GameObject.Find("Pellets").transform;
        }
        public void ResetGhostAndPacmanPositions() {
            MovementControl.playerTransform.position = GetStartingPacmanLocation();
            foreach(Ghost ghost in ghosts) {
                ghost.ResetGhost();
            }
        }
        public int NumberOfPelletsInLevel() {
            return pelletTransform.childCount;
        }
        private Vector3 GetStartingPacmanLocation() {
            float y = MovementControl.playerTransform.position.y;
            if(GameManager.selectedGameMode == (int)GameManager.Gamemodes.Story) {
                return new Vector3(pacmanSpawnStoryMode.x, y, pacmanSpawnStoryMode.y);
            }
            else {
                Vector2 pos = infiniteModeSpawnPoints[Random.Range(0, infiniteModeSpawnPoints.Count)];
                return new Vector3(pos.x, y, pos.y);
            }
        }
    }
}