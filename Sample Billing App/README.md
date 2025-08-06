# Sample Billing App - Invoice Printing System

A professional Windows Forms application for generating and printing invoices with a live HTML preview.

## 🏗️ Project Overview

This is a .NET 8.0 Windows Forms application that provides a complete invoice management system with the following features:

- **Live HTML Preview**: Real-time invoice preview using WebBrowser control
- **Professional Printing**: High-quality GDI+ printing that matches the HTML preview exactly
- **Sample Data Generation**: Random data generation for testing
- **Item Management**: Add, remove, and manage invoice items
- **Tax Calculations**: Automatic GST calculations (CGST 2.5%, SGST 2.5%)
- **Amount in Words**: Automatic conversion of amounts to words
- **Printer Selection**: Choose from available system printers

## 🎯 Key Features

### ✅ **Unified Design System**
- HTML preview and GDI+ printing use identical styling
- Consistent fonts, colors, layout, and spacing
- Professional invoice template matching industry standards

### ✅ **Live Preview**
- Real-time HTML preview as you add items
- Responsive design with proper styling
- Auto-refresh on data changes

### ✅ **Reliable Printing**
- GDI+ printing for consistent output
- Matches HTML preview exactly
- Professional formatting and layout

### ✅ **Data Management**
- Customer information management
- Item catalog with predefined products
- Quantity, MRP, and rate calculations
- Automatic total calculations

## 🚀 Installation & Setup

### Prerequisites
- .NET 8.0 SDK or Runtime
- Windows 10/11
- Visual Studio 2022 (for development)

### Quick Start
1. **Clone the repository**
   ```bash
   git clone https://github.com/nuhman6281/Sample-Billing-App-Demo.git
   cd Sample-Billing-App-Demo
   ```

2. **Build the application**
   ```bash
   dotnet build
   ```

3. **Run the application**
   ```bash
   dotnet run --project "Sample Billing App"
   ```

## 📁 Project Structure

```
Sample Billing App/
├── Models/
│   ├── Invoice.cs          # Invoice data model
│   └── InvoiceItem.cs      # Invoice item model
├── Services/
│   ├── PdfInvoiceGenerator.cs  # Printing service
│   └── InvoiceGenerator.cs     # HTML generation
├── Assets/
│   └── invoice_template.html   # HTML template
├── Form1.cs                    # Main form logic
├── Form1.Designer.cs           # Form designer
└── Sample Billing App.csproj   # Project file
```

## 🎨 User Interface

### Left Panel - Data Entry
- **Customer Details**: Name, mobile, GSTIN, payment type
- **Item Management**: Add/remove items with quantity, MRP, rate
- **Invoice Controls**: Print, clear, new invoice, load sample data

### Right Panel - Live Preview
- **HTML Preview**: Real-time invoice display
- **Professional Layout**: Matches printed output exactly
- **Auto-refresh**: Updates as you modify data

## 🔧 Technical Implementation

### HTML Preview System
```csharp
// GenerateInvoiceHtml method creates consistent HTML
private string GenerateInvoiceHtml(Invoice invoice)
{
    // HTML generation with CSS styling
    // Matches the printed output exactly
}
```

### GDI+ Printing System
```csharp
// DrawInvoiceHtml method replicates HTML layout
private static void DrawInvoiceHtml(Graphics g, Invoice invoice)
{
    // GDI+ drawing with same fonts, colors, layout
    // Ensures print output matches preview
}
```

### Data Models
```csharp
public class Invoice
{
    public int InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string CustomerName { get; set; }
    public List<InvoiceItem> Items { get; set; }
    public decimal BillAmount { get; set; }
    public string AmountInWords { get; }
}
```

## 🎯 Usage Guide

### 1. **Load Sample Data**
- Click "Load Sample Data" to generate random invoice data
- Useful for testing and demonstration

### 2. **Add Items**
- Select item from dropdown
- Enter quantity, MRP, and rate
- Click "Add Item" to add to invoice

### 3. **Preview Invoice**
- Click "Refresh Preview" to see the HTML preview
- Preview updates automatically when adding items

### 4. **Print Invoice**
- Select printer from dropdown
- Click "Print Invoice" to print
- Output matches preview exactly

## 🛠️ Development

### Building from Source
```bash
# Clone repository
git clone https://github.com/nuhman6281/Sample-Billing-App-Demo.git

# Navigate to project
cd Sample-Billing-App-Demo

# Restore dependencies
dotnet restore

# Build project
dotnet build

# Run application
dotnet run --project "Sample Billing App"
```

### Key Dependencies
- **.NET 8.0**: Modern .NET framework
- **System.Drawing**: GDI+ printing functionality
- **System.Windows.Forms**: Windows Forms UI

## 🎨 Design Features

### Professional Invoice Layout
- **Header**: Store name and details
- **Invoice Info**: Number, date, customer details
- **Items Table**: Product list with calculations
- **Totals**: Tax calculations and final amount
- **Footer**: Thank you message

### Consistent Styling
- **Fonts**: Arial family for readability
- **Colors**: Professional color scheme
- **Layout**: Clean, organized structure
- **Spacing**: Proper margins and padding

## 🔍 Error Handling

### Robust Error Management
- **Amount Conversion**: Safe fallbacks for amount-to-words
- **Printer Validation**: Checks for available printers
- **Data Validation**: Ensures required fields are filled
- **Exception Handling**: Graceful error messages

## 📊 Sample Data

The application includes predefined items:
- Basmati Rice, Sunflower Oil, Toor Dal
- Sugar, Tea Powder, Wheat Flour
- Cooking Oil, Salt, Spices Mix, Pulses Mix

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📄 License

This project is open source and available under the MIT License.

## 🆘 Support

For issues and questions:
- Create an issue on GitHub
- Check the documentation
- Review the code comments

## 🎉 Acknowledgments

- Built with .NET 8.0
- Uses Windows Forms for UI
- GDI+ for reliable printing
- HTML/CSS for preview rendering

---

**Built with ❤️ using .NET 8.0 and Windows Forms** 