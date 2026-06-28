using System;
using FluentAssertions;
using Xunit;

namespace Tasks.Tests;

public class DoublyLinkedListTests
{
    [Fact]
    public void Should_Increment_Length_Of_The_List_When_Element_Added()
    {
        DoublyLinkedList<int> list = [1, 2];

        int actualLength = list.Length;

        actualLength.Should().Be(2);
    }

    [Fact]
    public void Should_Return_Element_At_The_Specified_Position()
    {
        DoublyLinkedList<int> list = [5, 6];

        int actualElement = list.ElementAt(1);

        actualElement.Should().Be(6);
    }

    [Fact]
    public void Should_Throw_IndexOutOfRangeException_If_List_Is_Empty()
    {
        DoublyLinkedList<int> list = [];

        Action action = () => list.ElementAt(0);

        action.Should().ThrowExactly<IndexOutOfRangeException>();
    }

    [Fact]
    public void Should_Throw_IndexOutOfRangeException_If_Index_Bigger_Or_Equal_Than_Length()
    {
        DoublyLinkedList<int> list = [1, 3];

        Action action = () => list.ElementAt(2);

        action.Should().ThrowExactly<IndexOutOfRangeException>();
    }

    [Fact]
    public void Should_Throw_IndexOutOfRangeException_If_Index_Less_Than_Zero()
    {
        DoublyLinkedList<int> list = [1, 3];

        Action action = () => list.ElementAt(-2);

        action.Should().ThrowExactly<IndexOutOfRangeException>();
    }

    [Fact]
    public void Should_Insert_Element_At_Specified_Position()
    {
        DoublyLinkedList<int> list = [1, 3];

        list.AddAt(1, 5);
        int actualElement = list.ElementAt(1);

        actualElement.Should().Be(5);
    }

    [Fact]
    public void Should_Increment_Length()
    {
        DoublyLinkedList<int> list = [1];

        list.AddAt(1, 5);
        int actualElement = list.Length;

        actualElement.Should().Be(2);
    }

    [Fact]
    public void Should_Insert_Element_At_The_End_Of_The_List()
    {
        DoublyLinkedList<int> list = [1, 6];

        list.AddAt(2, 8);
        int actualElement = list.ElementAt(2);

        actualElement.Should().Be(8);
    }

    [Fact]
    public void Should_Insert_Element_At_The_Beginning_Of_The_List()
    {
        DoublyLinkedList<int> list = [1, 6];

        list.AddAt(0, 8);
        int actualElement = list.ElementAt(0);

        actualElement.Should().Be(8);
    }

    [Fact]
    public void Should_Throw_IndexOutOfRangeException_If_Index_Bigger_Than_Length()
    {
        DoublyLinkedList<int> list = [1, 6];

        Action action = () => list.ElementAt(2);

        action.Should().ThrowExactly<IndexOutOfRangeException>();
    }

    [Fact]
    public void Should_Remove_First_Occurance_In_The_List()
    {
        DoublyLinkedList<int> list = [1, 2, 1];

        list.Remove(1);

        list.Should().BeEquivalentTo([2, 1]);
    }

    [Fact]
    public void Should_Decrement_Length_Of_The_List_If_Element_Removed()
    {
        DoublyLinkedList<int> list = [1, 2];

        list.Remove(2);
        int actualLength = list.Length;

        actualLength.Should().Be(1);
    }

    [Fact]
    public void Should_Not_Change_Collection_If_It_Does_Not_Contain_Specified_Item()
    {
        DoublyLinkedList<int> list = [1, 2];

        list.Remove(4);

        list.Should().BeEquivalentTo([1, 2]);
    }

    [Fact]
    public void Should_Not_Decrement_Length_Of_The_List_If_It_Does_Not_Contain_Specified_Item()
    {
        DoublyLinkedList<int> list = [1, 2];

        list.Remove(4);
        int actualLength = list.Length;

        actualLength.Should().Be(2);
    }

    [Fact]
    public void Should_Remove_Element_At_The_Specified_Position()
    {
        DoublyLinkedList<int> list = [5, 6];

        list.RemoveAt(1);

        list.Should().BeEquivalentTo([5]);
    }

    [Fact]
    public void Should_Return_Removing_Item()
    {
        DoublyLinkedList<int> list = [2, 5];

        int removedItem = list.RemoveAt(1);

        removedItem.Should().Be(5);
    }

    [Fact]
    public void Should_Decrement_Length_Of_The_List_If_Element_Removed_Using_RemoveAt()
    {
        DoublyLinkedList<int> list = [1, 2];

        list.RemoveAt(1);
        int actualLength = list.Length;

        actualLength.Should().Be(1);
    }

    [Fact]
    public void Should_Throw_IndexOutOfRangeException_For_RemoveAt_If_List_Is_Empty()
    {
        DoublyLinkedList<int> list = [];

        Action action = () => list.RemoveAt(0);

        action.Should().ThrowExactly<IndexOutOfRangeException>();
    }

    [Fact]
    public void Should_Throw_IndexOutOfRangeException_If_Index_For_RemoveAt_Bigger_Or_Equal_Than_Length()
    {
        DoublyLinkedList<int> list = [2, 5];

        Action action = () => list.RemoveAt(2);

        action.Should().ThrowExactly<IndexOutOfRangeException>();
    }

    [Fact]
    public void Should_Throw_IndexOutOfRangeException_If_Index_For_RemoveAt_Less_Than_Zero()
    {
        DoublyLinkedList<int> list = [2, 5];

        Action action = () => list.RemoveAt(-1);

        action.Should().ThrowExactly<IndexOutOfRangeException>();
    }

    [Fact]
    public void Should_Iterate_In_ForEach_Loop()
    {
        DoublyLinkedList<int> list = [2, 5, 42];

        int i = 0;
        foreach (int item in list)
        {
            item.Should().Be(list.ElementAt(i));
            i++;
        }
    }
}
