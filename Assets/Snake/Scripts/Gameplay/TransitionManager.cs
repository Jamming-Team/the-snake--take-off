using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using XTools;

namespace Snake {
    public class TransitionManager : MonoBehaviour {

        [SerializeField] GameObject _oldWorld;
        [SerializeField] GameObject _newWorld;
        
        [SerializeField] SoundData _transitionSound;

        [SerializeField] Volume _volume;
        [SerializeField] float _effectDuration = 1;


        FilmGrain  _filmGrain;
        TransitionComponent _transitionComponent;
        bool _isIncreasing;
        AudioManager _audioManager;

        void Awake() {
            _volume.profile.TryGet(out _filmGrain);
        }

        void Start() {
            ServiceLocator.For(this).Get(out PlayerMediator mediator);
            _transitionComponent = mediator.transitionComponent;
            _transitionComponent.OnTransition += ChangeDeWorld;
            
            ServiceLocator.For(this).Get(out _audioManager);
        }

        void Update() {
            _filmGrain.intensity.value = tt;
        }


        // My final message..
        void ChangeDeWorld() {
            StopCoroutine(nameof(Pulse));
            StartCoroutine(nameof(Pulse), _effectDuration);
            _oldWorld.SetActive(!_oldWorld.activeSelf);
            _newWorld.SetActive(!_newWorld.activeSelf);
            
            _audioManager.PlaySound(_transitionSound);
        }

        float t = 0f;
        float tt = 0f;
        
        IEnumerator Pulse(float duration)
        {
            t = 0f;


            tt = 1f;
            
            // Increase from 0 to 1
            while (t < 1f)
            {
                t += Time.deltaTime / (duration / 2f); // half the duration for going up
                t = Mathf.Min(t, 1f); // clamp to 1
                yield return null;
            }

            // Decrease from 1 to 0
            while (t > 0f) {
                tt = t;
                t -= Time.deltaTime / (duration / 2f);
                t = Mathf.Max(t, 0f); // clamp to 0
                yield return null;
            }

            Debug.Log("Pulse complete");
        }

    }
}