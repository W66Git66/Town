using System;
using UnityEngine;
using System.Collections.Generic;

namespace WeatherControl{

    [Serializable]
        public class Season{
            [SerializeField] private int startMonth;
            [SerializeField] private int endMonth;
            [SerializeField] private float morningHour;
            [SerializeField] private float eveningHour;
            [SerializeField] private float nightHour;
            [SerializeField] private List<Phenomenon> atmosphericPhenomena;

            public int GetStartMonth(){ return startMonth; }
            public int GetEndMonth(){ return endMonth; }
            public float GetMorningHour(){ return morningHour; }
            public float GetEveningHour(){ return eveningHour; }
            public float GetNightHour(){ return nightHour; }
            public List<Phenomenon> GetPhenomenons(){ return atmosphericPhenomena; }
            public List<float> GetProbabilities(){ 
                
                List<float> probabilities = new List<float>();

                foreach(Phenomenon phenomenon in atmosphericPhenomena){
                    probabilities.Add(phenomenon.GetProbability());
                }

                return probabilities;
            }
            public List<ParticleSystem> GetParticleSystems(){ 
                List<ParticleSystem> particleSystems = new List<ParticleSystem>();

                foreach(Phenomenon phenomenon in atmosphericPhenomena){
                    particleSystems.Add(phenomenon.GetParticleSystem());
                }

                return particleSystems;
             }

            public Season(int startMonth, int endMonth, float morningHour, float eveningHour, float nightHour){
                    this.startMonth = startMonth;
                    this.endMonth = endMonth;
                    this.morningHour = morningHour;
                    this.eveningHour = eveningHour;
                    this.nightHour = nightHour;
            }   
        }
}