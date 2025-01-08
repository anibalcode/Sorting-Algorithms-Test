using System;
using System.Threading;

class SortingVisualizer
{
	static void Main()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("=== Sorting Visualizer ===");
			Console.WriteLine("Choose a sorting algorithm:");
			Console.WriteLine("1. Bubble Sort");
			Console.WriteLine("2. Merge Sort");
			Console.WriteLine("3. Quick Sort");
			Console.WriteLine("4. Exit");

			Console.Write("Enter your choice (1-4): ");
			string choice = Console.ReadLine();

			if (choice == "4")
			{
				Console.WriteLine("Exiting... Goodbye!");
				break;
			}

			Console.Write("Enter a list of integers separated by spaces: ");
			string input = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(input))
			{
				Console.WriteLine("Input cannot be empty. Press Enter to try again.");
				Console.ReadLine();
				continue;
			}

			string[] parts = input.Split(' ');
			int[] array;

			try
			{
				array = Array.ConvertAll(parts, int.Parse);
			}
			catch (FormatException)
			{
				Console.WriteLine("Invalid input. Please enter integers separated by spaces. Press Enter to try again.");
				Console.ReadLine();
				continue;
			}

			switch (choice)
			{
				case "1":
					Console.WriteLine("\nVisualizing Bubble Sort:");
					BubbleSort(array);
					break;
				case "2":
					Console.WriteLine("\nVisualizing Merge Sort:");
					MergeSort(array, 0, array.Length - 1);
					DisplayArray(array);
					break;
				case "3":
					Console.WriteLine("\nVisualizing Quick Sort:");
					QuickSort(array, 0, array.Length - 1);
					DisplayArray(array);
					break;
				default:
					Console.WriteLine("Invalid choice.");
					break;
			}

			Console.WriteLine("\nSorting complete. Press Enter to return to the main menu.");
			Console.ReadLine();
		}
	}

	static void DisplayArray(int[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(array[i] + " ");
		}
		Console.ResetColor();
		Console.WriteLine();
		Thread.Sleep(500); // Pause for visualization
	}

	static void BubbleSort(int[] array)
	{
		int n = array.Length;
		for (int i = 0; i < n - 1; i++)
		{
			for (int j = 0; j < n - i - 1; j++)
			{
				// Change color to indicate swap
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine($"Comparing {array[j]} and {array[j + 1]}");
				if (array[j] > array[j + 1])
				{
					// Swap
					int temp = array[j];
					array[j] = array[j + 1];
					array[j + 1] = temp;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Swapping {array[j]} and {array[j + 1]}");
				}
				DisplayArray(array);
			}
		}
	}

	static void MergeSort(int[] array, int left, int right)
	{
		if (left < right)
		{
			int mid = (left + right) / 2;

			// Visualize the recursion step
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"Dividing: left={left}, mid={mid}, right={right}");

			MergeSort(array, left, mid);
			MergeSort(array, mid + 1, right);

			Merge(array, left, mid, right);
		}
	}

	static void Merge(int[] array, int left, int mid, int right)
	{
		int n1 = mid - left + 1;
		int n2 = right - mid;

		int[] leftArray = new int[n1];
		int[] rightArray = new int[n2];

		for (int i = 0; i < n1; i++)
			leftArray[i] = array[left + i];
		for (int j = 0; j < n2; j++)
			rightArray[j] = array[mid + 1 + j];

		int i1 = 0, j1 = 0, k = left;

		// Merge two halves
		while (i1 < n1 && j1 < n2)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"Comparing {leftArray[i1]} and {rightArray[j1]}");

			if (leftArray[i1] <= rightArray[j1])
			{
				array[k] = leftArray[i1];
				i1++;
			}
			else
			{
				array[k] = rightArray[j1];
				j1++;
			}
			k++;
			DisplayArray(array);
		}

		while (i1 < n1)
		{
			array[k] = leftArray[i1];
			i1++;
			k++;
			DisplayArray(array);
		}

		while (j1 < n2)
		{
			array[k] = rightArray[j1];
			j1++;
			k++;
			DisplayArray(array);
		}
	}

	static void QuickSort(int[] array, int low, int high)
	{
		if (low < high)
		{
			int pi = Partition(array, low, high);

			QuickSort(array, low, pi - 1);
			QuickSort(array, pi + 1, high);
		}
	}

	static int Partition(int[] array, int low, int high)
	{
		int pivot = array[high];
		int i = (low - 1);

		for (int j = low; j < high; j++)
		{
			// Change color to indicate comparison
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"Comparing {array[j]} and pivot {pivot}");
			if (array[j] <= pivot)
			{
				i++;

				// Swap
				int temp = array[i];
				array[i] = array[j];
				array[j] = temp;
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"Swapping {array[i]} and {array[j]}");
			}
			DisplayArray(array);
		}

		// Swap pivot
		int temp1 = array[i + 1];
		array[i + 1] = array[high];
		array[high] = temp1;
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.WriteLine($"Placing pivot {array[i + 1]} at position {i + 1}");
		DisplayArray(array);

		return i + 1;
	}
}