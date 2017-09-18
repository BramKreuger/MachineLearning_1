using System;
using System.Collections.Generic;
using System.IO;

namespace MLassignment1Csharp
{
    public class KNNAssignment
    {

        List<Person> trainingData = new List<Person>();
        List<Person> testData = new List<Person>();

        public static double[] realMin = new double[6];
        public static double[] realMax= new double[6];

        static void Main(String[] args)
        {
            KNNAssignment main = new KNNAssignment();

            main.LoadFile("credit_train.txt", main.trainingData);
            main.LoadFile("credit_test.txt", main.testData);
            FindMinMaxRealNumbers(main.trainingData); //Deze commenten als je 2) wilt beantwoorden
            //main.PrintList(main.trainingData);
            AssignAllTestData(main.trainingData, main.testData);
            Console.ReadLine();
        }

        static void AssignAllTestData(List<Person> trainingPersons, List<Person> testPersons)
        {
            double changedYCount = 0;

            for(int i=0; i < testPersons.Count; i++) //Loop door de testdata heen
            {                
                double nearestNeighbourDist = double.MaxValue;
                Person nearestNeighbourPers = null;
                int j = 0;
                while(j < trainingPersons.Count - 1)
                {
                    double dist = ComputeDistance2(testPersons[i], trainingPersons[j]); //Deze commenten als je 1) wilt beantwoorden
                   // double dist = ComputeDistance(testPersons[i], trainingPersons[j]); //Deze commenten als je 2) wilt beantwoorden
                    if (dist < nearestNeighbourDist) 
                    {
                        nearestNeighbourDist = dist;
                        nearestNeighbourPers = trainingPersons[j];
                    }
                    j++;
                }
                if(testPersons[i].y != nearestNeighbourPers.y) //Tel de hoeveelheid veranderingen
                {
                    Console.WriteLine("ChangedY: " + nearestNeighbourDist);
                    changedYCount++;
                }
            testPersons[i].y = nearestNeighbourPers.y; //Assign de y waarde v/d dichtsbijzijnde train data                
            }
            Console.WriteLine(changedYCount + " from: " + testPersons.Count);
            Console.WriteLine(100 - changedYCount / testPersons.Count * 100);
        }

        //Voor opdracht 2
        static double ComputeDistance2(Person p1, Person p2)
        {

            double[] varDistances = new double[14];
            //Loop door de catagorische variabelen heen
            for (int i = 0; i < 4; i++)
            {
                varDistances[i] = Math.Pow((p1.xCategorical[i] - p2.xCategorical[i]), 2);
            }
            //Loop door de binaire variabelen heen
            for (int i = 0; i < 4; i++)
            {
                varDistances[i + 4] = Math.Pow((p1.xBinary[i] - p2.xBinary[i]), 2);
            }
            //Loop door de reeële variabelen heen
            for (int i = 0; i < 6; i++)
            {
                varDistances[i + 8] = Math.Pow((p1.xReal[i] - p2.xReal[i]) / (realMax[i] - realMin[i]), 2);
            }

            double distancesSum = 0;
            //Tel nu alle distances bij elkaar op 
            for (int i = 0; i < 14; i++)
            {                
                distancesSum = distancesSum + varDistances[i];
            }
            //Neem de wortel van de som
            return Math.Sqrt(distancesSum);            
        }

        //Voor opdracht 1
        static double ComputeDistance(Person p1, Person p2)
        {
            double[] varDistances = new double[14];
            //Loop door de catagorische variabelen heen
            for (int i = 0; i < 4; i++)
            {
                varDistances[i] = Math.Pow((p1.xCategorical[i] - p2.xCategorical[i]), 2);
            }
            //Loop door de binaire variabelen heen
            for (int i = 0; i < 4; i++)
            {
                varDistances[i + 4] = Math.Pow((p1.xBinary[i] - p2.xBinary[i]), 2);
            }
            //Loop door de reeële variabelen heen
            for (int i = 0; i < 6; i++)
            {
                varDistances[i + 8] = Math.Pow((p1.xReal[i] - p2.xReal[i]), 2);
            }

            double distancesSum = 0;
            //Tel nu alle distances bij elkaar op 
            for (int i = 0; i < 14; i++)
            {
                distancesSum = distancesSum + varDistances[i];
            }
            //Neem de wortel van de som
            return Math.Sqrt(distancesSum);
        }

        //Voor opdracht 2
        static void FindMinMaxRealNumbers(List<Person> trainingData) 
        {
            double[] minArray = new double[6];
            double[] maxArray = new double[6];
            for (int i = 0; i < 6; i++)
            {
                double max = 0;
                double min = double.MaxValue;
                for (int j = 0; j < trainingData.Count; j++)
                {
                    if (trainingData[j].xReal[i] > max)
                        max = trainingData[j].xReal[i];
                    if (trainingData[j].xReal[i] < min)
                        min = trainingData[j].xReal[i];
                }
                minArray[i] = min;
                maxArray[i] = max;
            }
            realMin = minArray;
            realMax = maxArray;
        }

        //Gegeven functie
        public void PrintList(List<Person> dataList)
        {
            foreach (Person person in dataList)
            {
                String personInfo = person.toString();
                Console.WriteLine(personInfo);
            }
        }

        //Gegeven functie
        public void LoadFile(String filename, List<Person> dataList)
        {
            StreamReader reader = null;

            try
            {
                reader = new StreamReader(filename);
                String text;

                while ((text = reader.ReadLine()) != null)
                {
                    Person person = new Person();
                    person.fromString(text);
                    dataList.Add(person);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                try
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }

}