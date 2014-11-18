#include <iostream>
using namespace std;

class arrayListType{
	
	public:

		bool isEmpty();
																					   //  Returns true if the list is empty; 
 																					   //  otherwise, returns false
		bool isFull();
																					   //  Returns true if the list is full; 
 																					   //  otherwise, returns false
		int listSize();
																					   //  Returns the size of the list, that is, the number 
																					   //  of elements currently in the list
		int maxListSize();
																					   //  Returns the maximum size of the list, that is, the 
																					   //  maximum number of elements that can be stored in 
 																					   //  the list
		void print() const;
																					   //  Outputs the elements of the list
		bool isItemAtEqual(int location, int item);
																					   //  If the item is same as the list element at position
																					   //  specified by location, returns true;
																					   //  otherwise return false
		void insertAt(int location, int insertItem);
																					   //  Insert an item in the list at the specified location.
																					   //  The item to be inserted and the location are passed 
																					   //  as parameter to the function.
																					   //  If the list is full or location is out of range,
																					   //  an appropriate message is displayed.
		void insertEnd(int insertItem);
																					   //  Insert an item at the end of the list. The parameter 
																					   //  insertItem specifies the item to be inserted.
																					   //  If the list is full, an appropriate message is 
																					   //  displayed.
		void removeAt(int location);
																					   //  Remove the item from the list at the position  
																					   //  specified by location. If location is out of range, 
																					   //  an appropriate message is printed.
		void retrieveAt(int location, int& retItem);
																					   //  Retrieve the element from the list at the  
																					   //  position specified by location. The item is 
																					   //  returned via the parameter retItem.
																					   //  If location is out of range, an appropriate 
																					   //  message is printed.
		void replaceAt(int location, int repItem);
																					   //  Replace the elements in the list at the position 
																					   //  specified by location. The item to be replaced is  
 																					   //  specified by the parameter repItem.
																					   //  If location is out of range, an appropriate 
																					   //  message is printed.
		void clearList();
																					   //  All elements from the list are removed. After this 
																					   //  operation the size of the list is zero.
		int seqSearch(int item);
  																					   //  Searches the list for a given item. If item is found, 
																					   //  returns location  in the array where the item is found; 
																					   //  otherwise returns -1.
		void insert(int insertItem);
																					   //  The item specified by the parameter insertItem is 
																					   //  inserted at the end of the list. However, first the
																					   //  list is searched to see if the item to be inserted is 
																					   //  already in the list. If the item is already in the list
																					   //  or the list is full, an appropriate message is output.
		void remove(int removeItem);
																					   //  Removes an item from the list. The item to be removed 
																					   //  is specified by the parameter removeItem.
		arrayListType(int size = 100);
																					   //  constructor
																					   //  Creates an array of size specified by the parameter
																					   //  size. The default array size is 100.
		arrayListType (const arrayListType& otherList); 
																					   //  copy constructor
		~arrayListType();
																					   //  destructor
																					   //  Deallocate the memory occupied by the array.

	protected:

		int *list; 	                                                                   //  array to hold the list elements
		int length;	                                                                   //  to store the length of the list
		int maxSize;	                                                               //  to store the maximum size of the list
};

/*
 * Member function definitions
 */

bool arrayListType::isEmpty()
{
	return (length == 0);
}

bool arrayListType::isFull()
{
	return (length == maxSize);
}

int arrayListType::listSize()
{
	return length;
}

int arrayListType::maxListSize()
{
	return maxSize;
}

void arrayListType::print() const
{
	int i;

	for(i = 0; i < length; i++)
		cout  <<  list[i]  <<  " ";
	cout  <<  endl;
}

bool arrayListType::isItemAtEqual(int location, int item)
{
	return (list[location] == item);
}

void arrayListType::insertAt(int location, int insertItem)
{
	int  i;

	if(location < 0 || location >= maxSize)										//  location out of range
		cout  <<  "The position of the item to be inserted "
			  <<  "is out of range"  <<  endl;
	else
		if(length >= maxSize)                                                  //  list is full
			cout  <<  "Cannot insert in a full list"  <<  endl;
		else
		{
			for(i = length; i > location; i--)
				list[i] = list[i - 1];	                                       //  move the elements down

			list[location] = insertItem;	                                   //  insert the item at the 
 										                                       //  specified position

			length++;														   //  increment the length
		}
}																			  //  end insertAt

void arrayListType::insertEnd(int insertItem)
{
	if(length >= maxSize)                                                       //  the list is full
		cout  <<  "Cannot insert in a full list"  <<  endl;
	else
	{
		list[length] = insertItem;												//  insert the item at the end
		length++;																//  increment length
	}
}																               //  end insertEnd


void arrayListType::removeAt(int location)
{
	int i;

	if(location < 0 || location >= length)
    	cout  <<  "The location of the item to be removed "
			  <<  "is out of range"  <<  endl;
	else
	{
   		for(i = location; i < length - 1; i++)
	 		list[i] = list[i+1];

		length--;
	}
}                                                                             //  end removeAt

void arrayListType::retrieveAt(int location, int& retItem)
{
	if(location < 0 || location >= length)
    	cout  <<  "The location of the item to be retrieved is "
			  <<  "out of range"  <<  endl;
	else
		retItem = list[location];
}                                                                             //   retrieveAt


void arrayListType::replaceAt(int location, int repItem)
{
	if(location < 0 || location >= length)
    	cout  <<  "The location of the item to be replaced is "
			  <<  "out of range"  <<  endl;
	else
		list[location] = repItem;

}                                                                             //  end replaceAt

void arrayListType::clearList()
{
	length = 0;
}                                                                             //   end clearList

arrayListType::arrayListType(int size)
{
	if(size < 0)
	{
 		cout  <<  "The array size must be positive. Creating "
 			  <<  "an array of size 100. "  <<  endl;

 		maxSize = 100;
 	}
 	else
 	   maxSize = size;

	length = 0;
	list = new int[maxSize];
}


arrayListType::~arrayListType()
{
	delete [] list;
}

	                                                                              //  copy constructor
arrayListType::arrayListType(const arrayListType& otherList)
{
	int j;

	maxSize = otherList.maxSize;
	length = otherList.length;
	list = new int[maxSize]; 	                                                  //  create the array

	if(length != 0) 				                                              //  if otherList is not empty
		for(j = 0; j < length; j++)                                               //  copy otherList
 			list[j] = otherList.list[j];
}                                                                                 //  end copy constructor


int arrayListType::seqSearch(int item)
{
	int loc;
	bool found = false;

	for(loc = 0; loc < length; loc++)
	   if(list[loc] == item)
	   {
		found = true;
		break;
	   }

	if(found)
		return loc;
	else
		return -1;
}                                                                             //  end search

void arrayListType::insert(int insertItem)
{
	int loc;

	if(length == 0)					                                          //  list is empty
		list[length++] = insertItem;                                          //  insert the item and 
 									                                          //  increment the length
	else
		if(length == maxSize)
			cout  <<  "Cannot insert in a full list."  <<  endl;
		else
		{
			loc = seqSearch(insertItem);

			if(loc == -1)                                                      //  the item to be inserted 
							                                                   //  does not exist in the list
				list[length++] = insertItem;
			else
				cout  <<  "the item to be inserted is already in "
 					  <<  "the list. No duplicates are allowed."  <<  endl;
	}
}                                                                             //  end insert


void arrayListType::remove(int removeItem)
{
	int loc;

	if(length == 0)
		cout  <<  "Cannot delete from an empty list."  <<  endl;
	else
	{
		loc = seqSearch(removeItem);

		if(loc != -1)
			removeAt(loc);
		else
			cout  <<  "The item to be deleted is not in the list."
				  <<  endl;
	}
}



int main(void)
{
	arrayListType list;																	// initialize array

	int num;																			// variable to hold inputted number
	int pos;																			// variable to hold inputted position
	int size = list.listSize();															// variable to size initialized to length

	cout << "Enter at least 10 integer values to insert into the list" << endl;			// instructions to user to input numbers
	cout << "Enter -999 to denote the end of the list" << endl;
	
	while (size < 10)																	// size check
	{
		cin >> num;

		while(num != -999)																// valid number check
		{
			list.insert(num);															// inserting number into list
	
			cin >> num;

		}
		size = list.listSize();															// recheck size
		if(size < 10)																	// display error message if not enough numbers
		{
			cout << "Less than 10 numbers. Please insert more numbers" << endl;
			num = 0;
		}
	}

	cout << endl << "The list is : " << endl;

	list.print();

	

	cout << "The list size is: " << size << endl << endl;
	
	cout << "Enter the item to be deleted" << endl;
	
	cin >> num;

	list.remove(num);

	cout << endl << "Enter the item to be deleted not in list" << endl;
	
	cin >> num;
	
	list.remove(num);

	cout << endl << "The list is : " << endl;

	list.print();

	size = list.listSize();

	cout << "The list size is: " << size << endl << endl;

	cout << "Enter an integer value to insert into the list" << endl;
	cin >> num;

	cout << "Enter the position in the list you want to add the item" << endl;

	cin >> pos;
	list.insertAt(pos, num);

	cout << endl << "The list is : " << endl;

	list.print();
	
	size = list.listSize();

	cout << "The list size is: " << size << endl << endl;

	cout << endl;
	system ("pause");

	return 0;										                                                                        
}

			                                                                           
