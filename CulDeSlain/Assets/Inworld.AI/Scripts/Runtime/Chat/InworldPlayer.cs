/*************************************************************************************************
* Copyright 2022 Theai, Inc. (DBA Inworld)
*
* Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
* that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
*************************************************************************************************/
using Inworld.Runtime;
using Inworld.Sample.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Inworld.Sample
{
    /// <summary>
    ///     This is the class for global text management, by original, it's added in Player Controller.
    ///     And would be called by Keycode.Backquote.
    /// </summary>
    public class InworldPlayer : InworldPlayer2D
    {
        #region Inspector Variables
        [SerializeField] InworldCameraController m_CameraController;
        [SerializeField] GameObject m_TriggerCanvas;
        [SerializeField] RecordButton m_RecordButton;
        [SerializeField] RuntimeCanvas m_RTCanvas;
        [SerializeField] Vector3 m_InitPosition;
        [SerializeField] Vector3 m_InitRotation;
        #endregion

        #region UI
        private Image m_Portrait;
        private TMPro.TMP_Text m_Name;
        private Transform m_Active;
        private Transform m_Passive;
        private TMPro.TMP_Text m_PassiveName;
        private bool isTalking = false;
        
        #endregion

        #region Public Function
        public void BackToLobby()
        {
            if (!m_RTCanvas)
                return;
            m_GlobalChatCanvas.gameObject.SetActive(false);
            m_CameraController.enabled = true;
            m_RTCanvas.gameObject.SetActive(true);
            m_RTCanvas.BackToLobby();
            Transform trPlayer = transform;
            trPlayer.position = m_InitPosition;
            trPlayer.eulerAngles = m_InitRotation;
        }
        #endregion

        #region Monobehavior Functions
        void Start()
        {
            InworldController.Instance.OnStateChanged += OnControllerStatusChanged;
            m_Passive = m_GlobalChatCanvas.transform.Find("Passive");
            m_Active = m_GlobalChatCanvas.transform.Find("Active");
            m_Portrait = m_Active.Find("PhotoBG").transform.Find("Portrait").GetComponent<Image>();
            m_Name = m_Active.Find("NameBG").transform.Find("Name").GetComponent<TMPro.TMP_Text>();
            m_PassiveName = m_Passive.Find("Tip").GetComponent<TMPro.TMP_Text>();
        }
        void Update()
        {
            var keyPressed = Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1);
            var closeness = InworldController.Instance.CurrentCharacter != null;
            if (closeness) {
                m_GlobalChatCanvas.SetActive(true);
                m_PassiveName.text = "Press 1 to talk to " + InworldController.Instance.CurrentCharacter.m_CharacterName;
                if (keyPressed) {
                    isTalking = !isTalking;
                    m_Passive.gameObject.SetActive(!isTalking);
                    m_Active.gameObject.SetActive(isTalking);
                    m_Portrait.sprite = InworldController.Instance.CurrentCharacter.m_Portrait;
                    m_Name.text = InworldController.Instance.CurrentCharacter.m_CharacterName;
                }
            } else {
                m_GlobalChatCanvas.SetActive(false);
            }
            UpdateSendText();
        }
        #endregion
    }
}
