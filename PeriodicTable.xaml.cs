using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml;


namespace Science.Chemistry
	{
  partial class WindowPeriodicTable : Window
		{
		XElement xdoc;
		AtomicElements AE = new AtomicElements();
		VisualBrush vb;
		
		public WindowPeriodicTable()
			{
			int maxColumns;
			int maxRows;
			
			InitializeComponent();
			ParseElementsXML();
			
			LinearGradientBrush lgbBackground = new LinearGradientBrush(Colors.Gainsboro, Colors.White, new Point(0,0), new Point(0,1));
			this.Background = lgbBackground;
			
			maxColumns = CalculateGroups();
			maxRows = CalculatePeriods();
			
			LayoutElementCells(maxColumns, maxRows);
			}
		
		private void ParseElementsXML()
			{
			XmlReader xr = XmlReader.Create(this.GetType().Assembly.GetManifestResourceStream("Science.PeriodicTable.xml"));
			xdoc = XElement.Load(xr);
						
			foreach(XElement xe in xdoc.Elements("Element"))
				{
				AtomicElement e = new AtomicElement
																{
																AtomicNumber = byte.Parse(xe.Attribute("Z").Value),
																Symbol = xe.Attribute("symbol").Value,
																Name = xe.Attribute("name").Value,
																Period = byte.Parse(xe.Attribute("period").Value),
																Group = byte.Parse(xe.Attribute("group").Value)
																};
				AE.Add(e);
				}
			}
		
		private void LayoutElementCells(int columns, int rows)
			{
			foreach(AtomicElement e in AE)
				{
				Button b = new Button();
				Grid g = new Grid();
				Viewbox v = new Viewbox();
				Label lblAtomicNumber = new Label();
				Label lblSymbol = new Label();
				Label lblName = new Label();
				Label lblAtomicMass = new Label();
				
				RowDefinition rd0 = new RowDefinition();
				RowDefinition rd1 = new RowDefinition();
				//RowDefinition rd2 = new RowDefinition();
				RowDefinition rd3 = new RowDefinition();
				rd0.Height = new GridLength(1, GridUnitType.Auto);
				rd1.Height = new GridLength(1, GridUnitType.Star);
				//rd2.Height = new GridLength(1, GridUnitType.Auto);
				rd3.Height = new GridLength(1, GridUnitType.Auto);
				g.RowDefinitions.Add(rd0);
				g.RowDefinitions.Add(rd1);
				//g.RowDefinitions.Add(rd2);
				g.RowDefinitions.Add(rd3);

				lblAtomicNumber.Content = e.AtomicNumber.ToString();
				lblAtomicNumber.VerticalAlignment = VerticalAlignment.Stretch;
				lblAtomicNumber.HorizontalAlignment = HorizontalAlignment.Left;
				lblAtomicNumber.HorizontalContentAlignment = HorizontalAlignment.Left;
				lblAtomicNumber.FontSize = 10;

				lblSymbol.Content = e.Symbol;
				lblSymbol.FontWeight = FontWeights.Bold;
				lblSymbol.VerticalAlignment = VerticalAlignment.Center;
				lblSymbol.VerticalContentAlignment = VerticalAlignment.Stretch;
				lblSymbol.HorizontalContentAlignment = HorizontalAlignment.Center;
				lblSymbol.FontSize = 18;
				v.Child = lblSymbol;
				v.Stretch = Stretch.Uniform;
				
				lblName.Content = e.Name;
				lblName.VerticalAlignment = VerticalAlignment.Stretch;
				lblName.HorizontalContentAlignment = HorizontalAlignment.Center;
				lblName.FontSize = 8;

				Grid.SetRow(lblAtomicNumber, 0);
				Grid.SetRow(v, 1);
				Grid.SetRow(lblName, 3);
				g.Children.Add(lblAtomicNumber);
				g.Children.Add(v);
				g.Children.Add(lblName);
								
				b.Content = g;
				//b.Margin = new Thickness(1.00);
				b.BorderBrush = Brushes.Black;
				b.BorderThickness = new Thickness(1.00);
				b.Click += new RoutedEventHandler(ElementButtonClicked);
								
				if ((e.Period == 6)&&(e.Group == 3))
					{
					Grid.SetColumn(b, e.Group + (e.AtomicNumber - 58));
					Grid.SetRow(b, e.Period + 2);
					}
				else if ((e.Period == 7) && (e.Group == 3))
					{
					Grid.SetColumn(b, e.Group + (e.AtomicNumber - 90));
					Grid.SetRow(b, e.Period + 3);
					}
				else
					{
					Grid.SetColumn(b, e.Group - 1);
					Grid.SetRow(b, e.Period - 1);
					}
				
				GridPeriodicTable.Children.Add(b);
				}
			}

		private int CalculatePeriods()
			{
			int a = 0;
			int max = 0;
			
			foreach (XAttribute att in xdoc.Elements("Element").Attributes("period"))
				{	max = Math.Max(a, Int32.Parse(att.Value)); }

			return max;
			}
		
		private int CalculateGroups()
			{
			int a = 0;
			int max = 0;
			
			foreach(XAttribute att in xdoc.Elements("Element").Attributes("group"))
				{	max = Math.Max(a, Int32.Parse(att.Value)); }
			
			return max;
			}
			
		void ElementButtonClicked(Object sender, RoutedEventArgs e)
			{
			Button btn = sender as Button;
			int col = (int)btn.GetValue(Grid.ColumnProperty);
			int row = (int)btn.GetValue(Grid.RowProperty);
			
			vb = new VisualBrush(btn);
			Rectangle r = new Rectangle();
			r.Height = btn.Height;
			r.Width = btn.Width;
			r.Fill = vb;
			r.Effect = new BlurEffect();
						
			Grid.SetColumn(r, col);
			Grid.SetRow(r, row);
			GridPeriodicTable.Children.Add(r);
			this.TryFindResource("");
			}
		}
	}
