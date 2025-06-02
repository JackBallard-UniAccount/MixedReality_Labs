using System.Collections.Generic;
using Meta.XR.EnvironmentDepth;
using Meta.XR.MRUtilityKit;
using TMPro;
using UnityEngine;

namespace DepthAPISample
{
    public class SceneMeshDepthMask : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI MaskDepthBiasText;
        [SerializeField] private OVRInput.RawButton _maskToggleButton = OVRInput.RawButton.B;
        [SerializeField] private OVRInput.RawButton _maskBiasAdjustDecreaseButton = OVRInput.RawButton.RThumbstickLeft;
        [SerializeField] private OVRInput.RawButton _maskBiasAdjustIncreaseButton = OVRInput.RawButton.RThumbstickRight;
        private EnvironmentDepthManager _environmentDepthManager;
        private float _maskBiasAdjustValue = 0.2f;
        private List<MeshFilter> _wallMeshFilters = new();

        private bool _isMaskOn;

        private void Start()
        {
            StartCoroutine(InitializeMeshMask());
        }

        private IEnumerator<WaitForSeconds> InitializeMeshMask()
        {
            // Wait until MRUK and the current room are initialized
            while (MRUK.Instance == null || MRUK.Instance.GetCurrentRoom() == null)
            {
                yield return new WaitForSeconds(0.1f);
            }

            // Wait until wall anchors and ceiling anchor are available
            while (MRUK.Instance.GetCurrentRoom().WallAnchors.Count == 0 ||
                   MRUK.Instance.GetCurrentRoom().CeilingAnchor == null)
            {
                yield return new WaitForSeconds(0.1f);
            }

            LoadRoomMesh();
            _isMaskOn = true; // Mark mask as active by default
        }

        private void Awake()
        {
            _environmentDepthManager = FindAnyObjectByType<EnvironmentDepthManager>();

            //remove hands from the depth map
            //_environmentDepthManager.RemoveHands = true;

            //restore hands in the depth map
            //_environmentDepthManager.RemoveHands = false;

        }

        private void LoadRoomMesh()
        {
            if (_environmentDepthManager == null)
                return;
            if ((MRUK.Instance.GetCurrentRoom() == null) || (_environmentDepthManager == null))
            {
                return;
            }
            _wallMeshFilters.Clear();
            for (var i = 1; i < MRUK.Instance.GetCurrentRoom().WallAnchors.Count; i++)
            {
                _wallMeshFilters.Add(MRUK.Instance.GetCurrentRoom().WallAnchors[i].gameObject.GetComponentInChildren<MeshFilter>());
            }

            _wallMeshFilters.Add(MRUK.Instance.GetCurrentRoom().CeilingAnchor.gameObject.GetComponentInChildren<MeshFilter>());

            _environmentDepthManager.MaskMeshFilters = _wallMeshFilters;
        }

        private void Update()
        {
            if (OVRInput.GetDown(_maskToggleButton))
            {
                if (!_isMaskOn)
                {
                    LoadRoomMesh();
                }
                else
                {
                    _wallMeshFilters.Clear();
                }
                _isMaskOn = !_isMaskOn;
            }

            if (OVRInput.Get(_maskBiasAdjustDecreaseButton))
            {
                _environmentDepthManager.MaskBias -= _maskBiasAdjustValue * Time.deltaTime;
            }

            if (OVRInput.Get(_maskBiasAdjustIncreaseButton))
            {
                _environmentDepthManager.MaskBias += _maskBiasAdjustValue * Time.deltaTime;
            }
            if (MaskDepthBiasText != null)
                MaskDepthBiasText.text = "Mask bias " + _environmentDepthManager.MaskBias.ToString("#.000");
        }
    }
}
