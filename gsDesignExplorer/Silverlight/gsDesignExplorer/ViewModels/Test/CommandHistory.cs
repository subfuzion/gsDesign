namespace gsDesign.Explorer.ViewModels.Test
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System.IO.IsolatedStorage;

	public class CommandHistory : IEnumerable, IEnumerable<string>
	{
		private readonly List<string> _buffer;
		private readonly int _capacity;
		private int _index;
		private int _count;

		public CommandHistory(int capacity = 100)
		{
			_buffer = new List<string>(capacity);
			_capacity = capacity;
		}

		public void Load()
		{
			using (var store = IsolatedStorageFile.GetUserStoreForApplication())
			{
				using (var stream = store.OpenFile("command.history", FileMode.OpenOrCreate, FileAccess.Read))
				{
					var r = new StreamReader(stream);
					
				}
			}
		}

		public void Save()
		{
			using (var store = IsolatedStorageFile.GetUserStoreForApplication())
			{
				using (var stream = store.OpenFile("command.history", FileMode.OpenOrCreate, FileAccess.Write))
				{
					var w = new StreamWriter(stream);

					var tail = Head - Count < 0 ? (_head - Count) % Capacity : _head - Count;
					var count = Count;

					for (var i = tail; count > 0; count--, i = (i + 1) % Capacity > 0 ? 0 : i + 1)
					{
						w.WriteLine(_buffer[i]);
					}
				}
			}
		}

		public IEnumerator<string> GetEnumerator()
		{
			var count = _count;

			if (count > 0)
			{
				var index = _head - 1 < 0 ? (_head - 1) % Capacity : _head - 1;

				for (var i = index; count > 0; count--, i = i - 1 < 0 ? Capacity - 1 : i - 1)
				{
					yield return _buffer[i];
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(string command)
		{
			if (_count < Capacity)
			{
				_buffer.Add(command);
				_count++;
			}
			else
			{
				_buffer[_head] = command;
			}

			// update index, wrap around if necesary
			_head = (_head % Capacity) == 0 ? 0 : _head + 1;
		}

		public void Clear()
		{
			_buffer.Clear();
			_count = 0;
			_head = 0;
		}

		public int Count { get { return _count; } }

		/// <summary>
		/// 0-based index returns last command, 1 returns previous, up to Count - 1
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public string this[int index]
		{
			get
			{
				if (index < 0 || index >= Count)
				{
					throw new IndexOutOfRangeException();
				}

				index = Head - 1 - index;
				index = index < 0 ? Capacity + index : index;
				return _buffer[index];
			}
		}

		/// <summary>
		/// The current write position
		/// </summary>
		private int Head
		{
			get { return _head; }
			set { _head = (value % Capacity) > 0 ? value % Capacity : value; }
		}

		/// <summary>
		/// The oldest command position
		/// </summary>
		private int Tail
		{
			get
			{
				var index = Head - Count;
			}
		}

		private int Capacity { get { return _capacity; } }

	}
}
