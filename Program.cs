using System;
using System.Collections.Generic;
using System.IO;

namespace MLassignment1Csharp
{
	public class KNNAssignment
	{
		List<Person> trainingData = new List<Person>();
		List<Person> testData     = new List<Person>();

		public static double[] realMin = new double[6];
		public static double[] realMax = new double[6];

		static void Main(String[] args)
		{
            for (int i = 1; i <= 5; i += 2) {
    			KNNAssignment main = new KNNAssignment();

    			main.LoadFile("credit_train.txt", main.trainingData);
    			main.LoadFile("credit_test.txt", main.testData);

                FindMinMaxRealNumbers(main.trainingData);
                AssignAllTestData(main.trainingData, main.testData, i);   
            }
		}

        // Standaard initialisatie.
		static void AssignAllTestData(List<Person> trainingPersons, List<Person> testPersons, int k)
		{
            double changedYCount = 0;

			//Loop door de testdata heen.
			for (int i = 0; i < testPersons.Count; i++) {
				double[] nearestNeighboursDist = new double[k];
				Person[] nearestNeighboursPers = new Person[k];
  
				int yTrue  = 0;
				int yFalse = 0;

				//Vul de arrays k keer.
				for (int j = 0; j < k; j++) {
					nearestNeighboursDist[j] = double.MaxValue;
				}

				for (int j = 0; j < trainingPersons.Count - 1; j++) {
					double dist      = ComputeDistance3(testPersons[i], trainingPersons[j]);
					bool changeIndex = false;
					int changedIndex = 0;

					for (int z = 0; z < k; z++) {
						if (dist < nearestNeighboursDist[z]) {
							changeIndex  = true;
							changedIndex = z;
						}
					}

					if (changeIndex) {
						nearestNeighboursDist[changedIndex] = dist;
						nearestNeighboursPers[changedIndex] = trainingPersons[j];
					}
				}

				foreach (Person neighbour in nearestNeighboursPers) {
					if (neighbour.y == 1) {
						yTrue += 1;
					} else {
						yFalse += 1;
					}
				}

				//Tel de hoeveelheid veranderingen.
				if (yTrue > yFalse) {
					if (testPersons[i].y != 1) {
						changedYCount += 1;
					}

					testPersons[i].y = 1;
                } else {
					if (testPersons[i].y != 0) {
						changedYCount += 1;
					}

					testPersons[i].y = 0;
				}
			}

			Console.WriteLine(100 - changedYCount / testPersons.Count * 100);
		}

		//Opdracht 1.
		static double ComputeDistance(Person p1, Person p2)
		{
			double[] varDistances = new double[14];
            double distancesSum   = 0;
			
            //Loop door de catagorische variabelen heen.
			for (int i = 0; i < 4; i++) {
				varDistances[i] = Math.Pow((p1.xCategorical[i] - p2.xCategorical[i]), 2);
			}
			
            //Loop door de binaire variabelen heen.
			for (int i = 0; i < 4; i++) {
				varDistances[i + 4] = Math.Pow((p1.xBinary[i] - p2.xBinary[i]), 2);
			}

			//Loop door de reeële variabelen heen.
			for (int i = 0; i < 6; i++) {
				varDistances[i + 8] = Math.Pow((p1.xReal[i] - p2.xReal[i]), 2);
			}

			//Tel nu alle distances bij elkaar op. 
			for (int i = 0; i < 14; i++) {
				distancesSum += varDistances[i];
			}

			//Neem de wortel van de som.
			return Math.Sqrt(distancesSum);
		}

		//Opdracht 2.
		static double ComputeDistance2(Person p1, Person p2)
		{
			double[] varDistances = new double[14];
			double distancesSum   = 0;

			//Loop door de catagorische variabelen heen.
			for (int i = 0; i < 4; i++) {
				varDistances[i] = Math.Pow((p1.xCategorical[i] - p2.xCategorical[i]), 2);
			}

			//Loop door de binaire variabelen heen.
			for (int i = 0; i < 4; i++) {
				varDistances[i + 4] = Math.Pow((p1.xBinary[i] - p2.xBinary[i]), 2);
			}

			//Loop door de reeële variabelen heen.
			for (int i = 0; i < 6; i++) {
				varDistances[i + 8] = Math.Pow((p1.xReal[i] - p2.xReal[i]) / (realMax[i] - realMin[i]), 2);
			}

			//Tel nu alle distances bij elkaar op. 
			for (int i = 0; i < 14; i++) {
				distancesSum += varDistances[i];
			}

			//Neem de wortel van de som.
			return Math.Sqrt(distancesSum);
		}

		//Opdracht 3.
		static double ComputeDistance3(Person p1, Person p2)
		{
			double[] varDistances = new double[14];
			double distancesSum   = 0;

            //Loop door de catagorische variabelen heen.
			for (int i = 0; i < 4; i++) {
                if (p1.xCategorical[i] == p2.xCategorical[i]) {
                    varDistances[i] = 0;
                } else {
                    varDistances[i] = 1;
                }
			}

			//Loop door de binaire variabelen heen.
			for (int i = 0; i < 4; i++) {
				varDistances[i + 4] = Math.Pow((p1.xBinary[i] - p2.xBinary[i]), 2);
			}

			//Loop door de reeële variabelen heen.
			for (int i = 0; i < 6; i++) {
				varDistances[i + 8] = Math.Pow((p1.xReal[i] - p2.xReal[i]) / (realMax[i] - realMin[i]), 2);
			}

			//Tel nu alle distances bij elkaar op. 
			for (int i = 0; i < 14; i++) {
				distancesSum += varDistances[i];
			}
			//Neem de wortel van de som.
			return Math.Sqrt(distancesSum);
		}

		//Functie opdracht 2.
		static void FindMinMaxRealNumbers(List<Person> trainingData)
		{
			double[] minArray = new double[6];
			double[] maxArray = new double[6];

			for (int i = 0; i < 6; i++) {
				double max = 0;
				double min = double.MaxValue;

				for (int j = 0; j < trainingData.Count; j++) {
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

		//Gegeven functie.
		public void PrintList(List<Person> dataList)
		{
			foreach (Person person in dataList)
			{
				String personInfo = person.toString();
				Console.WriteLine(personInfo);
			}
		}

		//Gegeven functie.
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