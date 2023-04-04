using Keras;
using Keras.Callbacks;
using Keras.Layers;
using Keras.Models;
using Numpy;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TestNetApi.MachineLearning
{
    [Flags]
    public enum TrainStatus
    {
        Running = 0,
        Stopped = 1,
        Finish = 2,
        NotExist = 4,
        Restarted = 8
        //Stopped = 16,
        //WaitSleepJoin = 32,
        //Suspended = 64,
        //AbortRequested = 128,
        //Aborted = 256
    }

    public static class TrainingHandler
    {

        private static Dictionary<string, Thread> trainConnections;
        private static Dictionary<string, Sequential> models;

        static TrainingHandler()
        {
            trainConnections = new Dictionary<string, Thread>();
            models = new Dictionary<string, Sequential>();
        }

        private static void MainTrainConnections(object? o)
        {

            //NDarray x = np.array(new float[,] { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } });
            //NDarray y = np.array(new float[] { 0, 1, 1, 0 });

            ////Build sequential model
            //var model = new Sequential();
            //model.Add(new Dense(32, activation: "relu", input_shape: new Shape(2)));
            //model.Add(new Dense(64, activation: "relu"));
            //model.Add(new Dense(1, activation: "sigmoid"));


            ////Compile and train
            //model.Compile(optimizer: "sgd", loss: "binary_crossentropy", metrics: new string[] { "accuracy" });
            //model.Fit(x, y, batch_size: 2, epochs: 1000, verbose: 1);
            //model.Save("models/test/model.h5");

            for (int i = 0; i < 60; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"train epoch {i}");
            }
        }

        public static TrainStatus GetTrainStatus(int id)
        {
            if (trainConnections.ContainsKey($"{id}"))
            {
                switch (trainConnections[$"{id}"].ThreadState)
                {
                    case ThreadState.WaitSleepJoin: return TrainStatus.Running;
                    case ThreadState.Stopped: return TrainStatus.Finish;
                    default: return 0;
                }
            }
            else
            {
                return TrainStatus.NotExist;
            }
        }

        public static TrainStatus TrainConnection(TimeSerias timeSerias)
        {
            //получает данные для обучения
            //подгататавливает данные для обучения
            //обучает модель для конкретного соединения
            //сохраняет модель для определенного узла

            ///Дополнительно проверять состояние других потоков

            //запуск тренировки
            var id = $"{timeSerias.id}";

            if (trainConnections.ContainsKey($"{id}"))
            {
                if(trainConnections[$"{id}"].ThreadState == ThreadState.WaitSleepJoin)
                {
                    return TrainStatus.Running;
                }
                else
                {
                    trainConnections.Remove(id);
                    trainConnections.Add(id, new Thread(new ParameterizedThreadStart(MainTrainConnections)));
                    trainConnections[id].Start();
                    return TrainStatus.Restarted;
                }
            }
            else
            {
                trainConnections.Add(id, new Thread(new ParameterizedThreadStart(MainTrainConnections)));
                trainConnections[id].Start();
                return TrainStatus.Running;
            }
        }

        public static async Task<TimeSerias> PredictConnection(TimeSerias timeSerias)
        {
            //загружает натренированную модель
            //получает данные для формирования прогноза
            //возвращает предсказание

            //Симуляция прогноза
            await Task.Delay(1000);

            var ts = new TimeSerias();
            ts.id = 1;

            ts.data.Add(1);
            ts.data.Add(2);
            ts.data.Add(3);
            ts.data.Add(4);
            ts.data.Add(5);
            ts.data.Add(6);

            return ts; 
        }
        private static void tensorflow()
        {
            NDarray x = np.array(new float[,] { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } });
            NDarray y = np.array(new float[] { 0, 1, 1, 0 });

            //Build sequential model
            var model = new Sequential();
            model.Add(new Dense(32, activation: "relu", input_shape: new Shape(2)));
            model.Add(new Dense(64, activation: "relu"));
            model.Add(new Dense(1, activation: "sigmoid"));

            //Compile and train
            model.Compile(optimizer: "sgd", loss: "binary_crossentropy", metrics: new string[] { "accuracy" });
            model.Fit(x, y, batch_size: 2, epochs: 1000, verbose: 1);

            //Save model and weights
            string json = model.ToJson();
            File.WriteAllText("model.json", json);
            model.SaveWeight("model.h5");

            //Load model and weight
            var loaded_model = Sequential.ModelFromJson(File.ReadAllText("model.json"));
            loaded_model.LoadWeight("model.h5");
        }
    }
}
