# TUKZO ABC - Invoice Printing System

A comprehensive Windows Forms application for creating and printing professional invoices with live preview functionality.

## 🎯 Project Overview

This application is designed for retail businesses to generate and print invoices with the following features:

- **Live Invoice Preview**: Real-time preview of the invoice as you add items
- **Professional Template**: Based on the exact template provided with proper formatting
- **Multiple Payment Options**: Support for CASH, CARD, UPI, and CHEQUE
- **Automatic Calculations**: GST calculations, savings, and amount in words
- **Printer Selection**: Choose from available system printers
- **Item Management**: Add/remove items with quantity and pricing controls

## 🏗️ Architecture

### **Project Structure**
```
Sample Billing App/
├── Models/
│   ├── Invoice.cs          # Main invoice model with calculations
│   └── InvoiceItem.cs      # Individual item model
├── Services/
│   └── InvoiceGenerator.cs # HTML generation service
├── Form1.cs               # Main application form
├── Form1.Designer.cs      # UI designer file
└── Program.cs             # Application entry point
```

### **Key Components**

#### **Models**
- **`Invoice`**: Complete invoice with customer details, items, and automatic calculations
- **`InvoiceItem`**: Individual items with pricing and quantity information

#### **Services**
- **`InvoiceGenerator`**: Generates HTML content matching the exact template format

#### **UI Features**
- **Left Panel**: Data entry with customer details, item management, and print controls
- **Right Panel**: Live HTML preview of the invoice
- **Split Container**: Resizable interface for optimal workflow

## 🚀 Features

### **Customer Management**
- Customer name, mobile number, and GSTIN
- Payment type selection (CASH, CARD, UPI, CHEQUE)
- Automatic invoice numbering and timestamp

### **Item Management**
- Pre-loaded item catalog with common products
- Customizable item descriptions
- Quantity, MRP, and rate controls
- Automatic total calculations
- Add/remove items with confirmation

### **Invoice Calculations**
- **Net Total**: Sum of all item totals
- **Savings**: Difference between MRP and selling price
- **GST Calculations**: 5% GST with CGST and SGST breakdown
- **Amount in Words**: Automatic conversion to text format
- **Total Quantity**: Sum of all item quantities

### **Printing Features**
- **Printer Selection**: Dropdown with all available system printers
- **Professional Output**: Exact template formatting
- **Error Handling**: Comprehensive error management for print operations

### **User Interface**
- **Responsive Design**: Split container with resizable panels
- **Live Preview**: Real-time invoice preview as you type
- **Intuitive Controls**: Clear labeling and logical workflow
- **Confirmation Dialogs**: Safe operations with user confirmation

## 📋 Requirements

### **System Requirements**
- **Operating System**: Windows 10/11
- **Framework**: .NET 8.0
- **Memory**: 512MB RAM minimum
- **Storage**: 50MB free space
- **Display**: 1200x800 minimum resolution

### **Prerequisites**
- Visual Studio 2022 or later
- .NET 8.0 SDK
- Windows Forms development tools

## 🛠️ Installation & Setup

### **Development Setup**

1. **Clone the Repository**
   ```bash
   git clone [repository-url]
   cd "Sample Billing App"
   ```

2. **Open in Visual Studio**
   - Open `Sample Billing App.sln` in Visual Studio 2022
   - Restore NuGet packages if prompted

3. **Build and Run**
   - Press `F5` or click "Start Debugging"
   - The application will launch with the main form

### **Production Deployment**

1. **Build Release Version**
   ```bash
   dotnet build --configuration Release
   ```

2. **Deploy Executable**
   - Copy the built executable from `bin/Release/net8.0-windows/`
   - Ensure target machine has .NET 8.0 Runtime installed

## 📖 Usage Guide

### **Creating a New Invoice**

1. **Enter Customer Details**
   - Fill in customer name, mobile, and GSTIN
   - Select payment type from dropdown

2. **Add Items**
   - Select item from the dropdown
   - Modify description if needed
   - Set quantity, MRP, and rate
   - Click "Add Item" to add to invoice

3. **Review and Print**
   - Review the live preview on the right panel
   - Select printer from dropdown
   - Click "Print Invoice" to print

### **Managing Items**

- **Add Item**: Select item, set details, click "Add Item"
- **Remove Item**: Select item in list, click "Remove Item"
- **Clear All**: Click "Clear Items" to remove all items
- **New Invoice**: Click "New Invoice" to start fresh

### **Printing Options**

- **Printer Selection**: Choose from available system printers
- **Print Preview**: Review invoice in right panel before printing
- **Error Handling**: Application handles print errors gracefully

## 🔧 Configuration

### **Default Settings**
- **Store Name**: "TUKZO ABC" (configurable in code)
- **GST Rate**: 5% (2.5% CGST + 2.5% SGST)
- **Invoice Template**: Based on provided HTML template
- **Default Payment**: CASH

### **Customization Options**
- Modify `InvoiceGenerator.cs` to change store details
- Update `LoadAvailableItems()` in `Form1.cs` for different products
- Adjust GST calculations in `Invoice.cs` model

## 🧪 Testing

### **Manual Testing Checklist**
- [ ] Customer details entry and validation
- [ ] Item addition and removal
- [ ] Price calculations accuracy
- [ ] GST calculations
- [ ] Live preview functionality
- [ ] Print functionality with different printers
- [ ] Error handling for invalid inputs
- [ ] New invoice creation
- [ ] Clear items functionality

### **Test Scenarios**
1. **Basic Invoice**: Add 2-3 items, verify calculations
2. **Empty Invoice**: Try to print without items
3. **Large Quantities**: Test with high quantity values
4. **Decimal Values**: Test with fractional quantities
5. **Special Characters**: Test with special characters in names

## 🐛 Troubleshooting

### **Common Issues**

#### **Print Not Working**
- Verify printer is connected and online
- Check printer drivers are installed
- Ensure printer supports the required paper size

#### **Preview Not Updating**
- Check if WebBrowser control is properly initialized
- Verify HTML generation is working correctly
- Restart application if preview becomes unresponsive

#### **Calculation Errors**
- Verify numeric input validation
- Check for decimal precision issues
- Ensure GST calculations are correct

### **Error Messages**
- **"Please select a printer first"**: Choose a printer from dropdown
- **"Error printing invoice"**: Check printer connection and drivers
- **"Are you sure you want to clear all items?"**: Confirmation dialog

## 📊 Performance Considerations

### **Optimization Tips**
- **Memory Management**: Dispose of WebBrowser controls properly
- **UI Responsiveness**: Use async operations for long-running tasks
- **Print Performance**: Optimize HTML generation for faster printing

### **Scalability**
- **Item Catalog**: Can be extended to load from database
- **Invoice Storage**: Can be enhanced with database persistence
- **Multi-user Support**: Can be extended for network deployment

## 🔒 Security Considerations

### **Data Protection**
- **Input Validation**: All user inputs are validated
- **Error Handling**: Comprehensive exception handling
- **No Sensitive Data**: Application doesn't store sensitive information

### **Best Practices**
- **Input Sanitization**: All inputs are properly validated
- **Error Logging**: Errors are logged and displayed appropriately
- **User Confirmation**: Critical operations require user confirmation

## 📈 Future Enhancements

### **Planned Features**
- **Database Integration**: Store invoices and customer data
- **Receipt Templates**: Multiple invoice template options
- **Barcode Support**: Add barcode generation for items
- **Email Integration**: Send invoices via email
- **Backup/Restore**: Invoice data backup functionality

### **Advanced Features**
- **Multi-language Support**: Localization for different languages
- **Cloud Integration**: Sync with cloud storage
- **Mobile App**: Companion mobile application
- **Analytics Dashboard**: Sales and inventory analytics

## 🤝 Contributing

### **Development Guidelines**
- Follow C# coding conventions
- Add proper error handling
- Include XML documentation
- Test thoroughly before submitting changes

### **Code Standards**
- **Naming**: Use PascalCase for classes, camelCase for variables
- **Comments**: Add comments for complex logic
- **Error Handling**: Use try-catch blocks appropriately
- **UI Design**: Follow Windows Forms design guidelines

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

For technical support or feature requests:
- **Email**: [support@tukzoabc.com]
- **Documentation**: [link-to-documentation]
- **Issues**: [GitHub issues page]

---

**Version**: 1.0.0  
**Last Updated**: December 2024  
**Compatibility**: .NET 8.0, Windows 10/11 