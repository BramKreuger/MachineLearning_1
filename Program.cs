using System;
using System.Collections.Generic;
using System.IO;

namespace MLassignment1Csharp
{
    public class KNNAssignment
    {

        List<Person> trainingData = new List<Person>();
        List<Person> testData = new List<Person>();

        static void Main(String[] args)
        {
            KNNAssignment main = new KNNAssignment();

            main.LoadFile("credit_train.txt", main.trainingData);
            main.LoadFile("credit_test.txt", main.testData);
            //main.PrintList(main.trainingData);
            AssignAllTestData(main.trainingData, main.testData);
            ComputeDistance(main.trainingData[0], main.trainingData[1]);
            Console.ReadLine();
        }

        static void AssignAllTestData(List<Person> trainingPersons, List<Person> testPersons)
        {
            int changedYCount = 0;

            foreach (Person pTest in testPersons) //Loop door de testdata heen
            {                
                double nearestNeighbourDist = double.MaxValue;
                Person nearestNeighbourPers = null;
                foreach (Person pTrain in trainingPersons) //Loop door de trainingsdata heen
                {
                    double dist = ComputeDistance(pTest, pTrain);
                    if (dist < nearestNeighbourDist) 
                    {
                        nearestNeighbourDist = dist;
                        nearestNeighbourPers = pTrain;
                    }                    
                }
                if(pTest.y != nearestNeighbourPers.y) //Tel de hoeveelheid veranderingen
                {
                    changedYCount++;
                }
                pTest.y = nearestNeighbourPers.y; //Assign de y waarde v/d dichtsbijzijnde train data                
            }
            Console.WriteLine(changedYCount);
            Console.WriteLine(changedYCount / testPersons.Count * 100);
        }

        static double ComputeDistance(Person p1, Person p2)
        {
            double[] varDistances = new double[16];
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
                varDistances[i + 4] = Math.Pow((p1.xReal[i] - p2.xReal[i]), 2);
            }

            double distancesSum = 0;
            //Tel nu alle distances bij elkaar op 
            for (int i = 0; i < 14; i++)
            {
                distancesSum =+ varDistances[1];
            }
            //Neem de wortel van de som
            return Math.Sqrt(distancesSum);            
        }

        public void PrintList(List<Person> dataList)
        {
            foreach (Person person in dataList)
            {
                String personInfo = person.toString();
                Console.WriteLine(personInfo);
            }
        }

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