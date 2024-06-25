using System;
using System.Collections.Generic;
using System.Text;

    public record InventoryItem(string Name, int Quantity, string Category); 

public class InventoryManager
{
    private List<InventoryItem> inventory;

    public InventoryManager()
    {
        inventory = new List<InventoryItem>();
    }

    public string CategorizeItem(InventoryItem item)
    {
        return item.Category switch
        {
            "Electronics" => "Electronics",
            "Clothing" => "Clothing",
            "Office Supplies" => "Office Supplies",
            _ => "Other"
        };
    }

    public void AddItem(InventoryItem item)
    {
        inventory.Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        inventory.Remove(item);
    }

    public string GenerateInventoryReport()
    {
        StringBuilder report = new StringBuilder();
        foreach (var item in inventory)
        {
            report.AppendLine($"Item: {item.Name}, Quantity: {item.Quantity}, Category: {item.Category}");
        }
        return report.ToString();
    }
}

class Program
{
    static void Main(string[] args)
    {
        InventoryManager manager = new InventoryManager();

        // Adding items to the inventory
        manager.AddItem(new InventoryItem("Shirt", 10, "Clothing"));
        manager.AddItem(new InventoryItem("Printer", 3, "Electronics"));

        // Generating and displaying inventory report
        string report = manager.GenerateInventoryReport();
        Console.WriteLine("Inventory Report:");
        Console.WriteLine(report);
    }
}
}