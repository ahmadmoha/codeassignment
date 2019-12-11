
using UnityEngine;
using System.Collections;
using System;

namespace ServiceLocator
{
    public class ServiceLocator : MonoBehaviour
    {
        void Start()
        {
            
            TheAudioPlayer audio = new TheAudioPlayer();
            ServiceLocator.RegisterService(audio);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var audio=ServiceLocator.GetAudioService();
                if (audio!=null)
                {
                    audio.PlaySound(1);
                }
            }

            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                var audio = ServiceLocator.GetAudioService();
                if (audio != null)
                {
                    audio.StopSound(1);
                }
            }

            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                var audio = ServiceLocator.GetAudioService();
                if (audio != null)
                {
                    audio.StopAllSounds();
                }
            }

            
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ServiceLocator.EnableAudioLogging();
            }
        }


    }



    
    public class ServiceLocator
    {
        static IAudio AudioService_;
        static NullAudio NullAudioService_;

        public static IAudio GetAudioService() { return AudioService_; }

        
        
        public static void RegisterService(IAudio service)
        {
            if (service == null)
            {
                // Revert to null service.
                AudioService_ = NullAudioService_;
            }
            else
            {
                AudioService_ = service;
            }
            Debug.Log("[ServiceLocator]Finish Register Service!");
        }

        
        public static void EnableAudioLogging()
        {
            // Decorate the existing service.
            IAudio service = new LoggedAudio(ServiceLocator.GetAudioService());

            // Swap it in.
            RegisterService(service);
        }

    }



    public interface IAudio
    {
        void PlaySound(int soundID);
        void StopSound(int soundID);
        void StopAllSounds();
    };


    public class TheAudioPlayer : IAudio
    {
        public void PlaySound(int soundID)
        {
            // Play sound using console audio 
            Debug.Log("Play Sound ! ID = "+soundID.ToString());
        }

        public void StopSound(int soundID)
        {
            // Stop sound using console audio
            Debug.Log("Stop Sound ! ID = " + soundID.ToString());
        }

        public void StopAllSounds()
        {
            // Stop all sounds using console audio
            Debug.Log("Stop All Sound ! ");
        }
    };


    public class NullAudio : IAudio
    {
        public void PlaySound(int soundID) { /* Do nothing. */ }
        public void StopSound(int soundID) { /* Do nothing. */ }
        public void StopAllSounds() { /* Do nothing. */ }
    };


    class LoggedAudio : IAudio
    {

        IAudio wrapped_;
        public LoggedAudio(IAudio wrapped)
        {
            wrapped_ = wrapped;
        }

        public void PlaySound(int soundID)
        {
            Log("[LoggedAudio]Play sound!");
            wrapped_.PlaySound(soundID);
        }

        public void StopSound(int soundID)
        {
            Log("[LoggedAudio]Stop sound!");
            wrapped_.StopSound(soundID);
        }

        public void StopAllSounds()
        {
            Log("[LoggedAudio]Stop all sounds!");
            wrapped_.StopAllSounds();
        }

        private void Log(string message)
        {
            Debug.LogError(message);
            // to log message...
        }
    }



}



