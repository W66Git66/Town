using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


namespace WeatherControl{
    public class WeatherControl : MonoBehaviour
    {        
        private GameObject weatherControl;
        [Header("Light")]
        [SerializeField] private UnityEngine.Rendering.Universal.Light2D sunLight;

        [Header("UI Elements")]
        [SerializeField] private Text clockText;
        [SerializeField] private Text calendarText;

        [Header("Time")]
        [SerializeField] private int hour = 0;
        [SerializeField] private int minute = 0;
        [SerializeField] private float secondsPerMinute = 1f;
        [SerializeField] private bool realTimeClock = false;

        
        [Header("Day Colors")]
        [SerializeField] private Color morningLight = new Color(1f, 0.9280525f, 0.6289308f, 1f);
        [SerializeField] private Color eveningLight = new Color(0.4921679f, 0.5660581f, 0.990566f);
        [SerializeField] private Color nightLight = new Color(0.4921679f, 0.5660581f, 0.990566f);

        [Header("Date")]
        [SerializeField] private bool weeksAndMonths = true;   
        [SerializeField] private int currentDay = 1;
        [SerializeField] private int currentMonth = 1;    
        [SerializeField] private Months months;
                    
        private Season currentSeason;

        [Header("Seasons")]
        [SerializeField] private bool seasons = true;
        [Range(0, 1)]
        [SerializeField] private float interpolationFactor = 0.3f;    
        [SerializeField] private SeasonsSettings seasonsSettings;
        [SerializeField] private NoSeasonsSettings noSeasonsSettings;
            

        private List<ParticleSystem> weatherParticleSystemOptions;

        private List<float> weatherProbabilities;  

        private int season = 1;
        private int weekDay = 1;
        private Color weatherColor = Color.white;
        private float lerpNormalization;
        private bool launchCoroutine = false;
        private Phenomenon currentPhenomenon;
       
        void Start()
        {
            weatherControl = GameObject.FindWithTag("weatherControl");

            if (realTimeClock){
                hour = System.DateTime.Now.Hour;
                minute = System.DateTime.Now.Minute;
                currentMonth = System.DateTime.Now.Month;
                currentDay = System.DateTime.Now.Day;
            }
            if(!seasons){
                weatherProbabilities = noSeasonsSettings.GetNoSeason().GetProbabilities();
                weatherParticleSystemOptions = noSeasonsSettings.GetNoSeason().GetParticleSystems();
            }

            launchCoroutine = true;
        }

        void Update()
        {
            TimeAndCalendarFormatChecking();
           
            if (launchCoroutine && !realTimeClock){
                launchCoroutine = false;
                StartCoroutine("ClockSimulation");
            }
            WeatherTimeController();
            
            if (seasons)
            {
                SetSeason();
            }
        }


        IEnumerator ClockSimulation() 
        {
            yield return new WaitForSeconds(secondsPerMinute);

            minute = minute + 1;

            if(minute == 60){
                minute = 0;
                hour = hour + 1;
            }

            launchCoroutine = true;
        }

        static int GetIntegerDigitCountString(int value)
        {
            return value.ToString().Length;
        }

        private void RealTimeController(){
            TimeController();
            WeatherController();
        }

        private void SimulatedTimeController(){
            TimeController();
            WeatherController();
        }

        private void TimeAndCalendarFormatChecking(){
            string minuteString = "";
            string hourString = "";

            if(realTimeClock){
                hour = System.DateTime.Now.Hour;
                minute = System.DateTime.Now.Minute;
                currentMonth = System.DateTime.Now.Month;
                currentDay = System.DateTime.Now.Day;
            }
            
            if (GetIntegerDigitCountString(minute) == 1){
                minuteString = "0" +  minute.ToString();
            }else {
                minuteString = minute.ToString();
            }

            if (GetIntegerDigitCountString(hour) == 1){
                hourString = "0" + hour.ToString();
            }else {
                hourString = hour.ToString();
            }

            clockText.text = hourString + ":" +  minuteString;

            if(weeksAndMonths) {
                if(seasons)
                    calendarText.text = Enum.GetName(typeof(Days), weekDay - 1) + ", " + months.GetMonths()[currentMonth-1].GetName() + ", " + currentDay + ". " + Enum.GetName(typeof(Seasons), season - 1);
                else
                    calendarText.text = Enum.GetName(typeof(Days), weekDay - 1) + ", " + months.GetMonths()[currentMonth-1].GetName() + ", " + currentDay;
            }else {
                calendarText.text = "Day: " + currentDay;
            }
        }


        private void SetSeason()
        {
            if(currentMonth <= 0){
                currentMonth = 1;
            }else if(currentMonth > months.GetNumberOfMonths()){
                currentMonth = months.GetNumberOfMonths();
            }

            if(currentMonth >= seasonsSettings.GetSpring().GetStartMonth() && currentMonth <= seasonsSettings.GetSpring().GetEndMonth()){
                season = 1;
                currentSeason = seasonsSettings.GetSpring();
                weatherProbabilities = seasonsSettings.GetSpring().GetProbabilities();
                weatherParticleSystemOptions = seasonsSettings.GetSpring().GetParticleSystems();
            }else if(currentMonth >= seasonsSettings.GetSummer().GetStartMonth() && currentMonth <= seasonsSettings.GetSummer().GetEndMonth()){
                season = 2;
                currentSeason = seasonsSettings.GetSummer();
                weatherProbabilities = seasonsSettings.GetSummer().GetProbabilities();
                weatherParticleSystemOptions = seasonsSettings.GetSummer().GetParticleSystems();
            }else if (currentMonth >= seasonsSettings.GetAutumn().GetStartMonth() && currentMonth <= seasonsSettings.GetAutumn().GetEndMonth()){
                season = 3;
                currentSeason = seasonsSettings.GetAutumn();
                weatherProbabilities = seasonsSettings.GetAutumn().GetProbabilities();
                weatherParticleSystemOptions = seasonsSettings.GetAutumn().GetParticleSystems();
            }else{
                season = 4;
                currentSeason = seasonsSettings.GetWinter();
                weatherProbabilities = seasonsSettings.GetWinter().GetProbabilities();
                weatherParticleSystemOptions = seasonsSettings.GetWinter().GetParticleSystems();
            }
        }

        private void TimeController() {
            
            float morningHour = 8;
            float eveningHour = 18;
            float nightHour = 21;

            if(currentSeason != null){
                morningHour = currentSeason.GetMorningHour();
                eveningHour = currentSeason.GetEveningHour();                
            }

            if(hour >= 0 && hour < morningHour) {
                lerpNormalization = (hour - 0f) / (morningHour - 0f);
                lerpNormalization = lerpNormalization * lerpNormalization * (3f - 2f * lerpNormalization);
                weatherColor = Color.Lerp(nightLight, morningLight, lerpNormalization);                
            } else if(hour >= morningHour && hour <= eveningHour) {
                lerpNormalization = (hour - morningHour) / (eveningHour - morningHour);
                lerpNormalization = lerpNormalization * lerpNormalization * (3f - 2f * lerpNormalization);
                weatherColor = Color.Lerp(morningLight, eveningLight, lerpNormalization);    
            } else if(hour > eveningHour && hour <= nightHour) { 
                lerpNormalization = (hour - eveningHour) / (nightHour - eveningHour);
                lerpNormalization = lerpNormalization * lerpNormalization * (3f - 2f * lerpNormalization);
                weatherColor = Color.Lerp(eveningLight, nightLight, lerpNormalization);    
                    
            }else if(hour > nightHour && hour <= 24) { 
                lerpNormalization = (hour - nightHour) / (24f - nightHour);
                lerpNormalization = lerpNormalization * lerpNormalization * (3f - 2f * lerpNormalization);
                weatherColor = Color.Lerp(nightLight, nightLight, lerpNormalization);    
                    
            }
        }

        private void WeatherController() {

            if(currentPhenomenon != null && currentPhenomenon.GetParticleSystem().isPlaying){
                weatherColor = Color.Lerp(weatherColor, currentPhenomenon.GetColor(), interpolationFactor);
            }

            //sunLight.color = weatherColor;  

            if(hour >= 24){
                hour = 0;
                currentDay = currentDay + 1;
                weekDay = weekDay + 1;

                float weatherValue = UnityEngine.Random.value;            
                float[] probabilities = new float[weatherParticleSystemOptions.Count];

                for(int i = 0; i < weatherParticleSystemOptions.Count; i++){
                    probabilities[i] = weatherProbabilities[i] - weatherValue;
                }

                var probabilitiesPassed = from value in probabilities where value > 0 select value;

                if(probabilitiesPassed.Count() > 0){
                    if(currentPhenomenon != null && currentPhenomenon.GetParticleSystem().isPlaying){
                        currentPhenomenon.GetParticleSystem().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    }
                    var diffList = probabilitiesPassed.Select(x => new { n = x, diff = Math.Abs(x - 0f) });
                    var result = diffList.Where(x => x.diff == diffList.Select(y => y.diff).Min()).First();
                    weatherParticleSystemOptions[Array.IndexOf(probabilities, result.n)].Play();
                    if(!seasons){
                        currentPhenomenon = noSeasonsSettings.GetNoSeason().GetPhenomenons()[Array.IndexOf(probabilities, result.n)];
                    }else if(season == 1){
                        currentPhenomenon = seasonsSettings.GetSpring().GetPhenomenons()[Array.IndexOf(probabilities, result.n)];
                    }else if(season == 2){
                        currentPhenomenon = seasonsSettings.GetSummer().GetPhenomenons()[Array.IndexOf(probabilities, result.n)];
                    }else if(season == 3){
                        currentPhenomenon = seasonsSettings.GetAutumn().GetPhenomenons()[Array.IndexOf(probabilities, result.n)];
                    }else if(season == 4){
                        currentPhenomenon = seasonsSettings.GetWinter().GetPhenomenons()[Array.IndexOf(probabilities, result.n)];
                    }
                }else{
                    if(currentPhenomenon != null && currentPhenomenon.GetParticleSystem().isPlaying){
                        currentPhenomenon.GetParticleSystem().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    }
                }
            }

            if (currentDay > months.GetMonths()[currentMonth-1].GetDays()) {
                if(weeksAndMonths)
                    currentMonth = currentMonth + 1;
                currentDay = 1;
            }

            if (weekDay > 7 && weeksAndMonths){
                weekDay = 1;
            }

            if (currentMonth > months.GetNumberOfMonths() && weeksAndMonths){
                currentMonth = 1;
            }
        }

        private void WeatherTimeController() {
            TimeController();
            WeatherController();
        }
    }
}