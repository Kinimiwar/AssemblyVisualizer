// Adapted, originally created as part of ILSpy project.
//
// Copyright (c) 2011 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.Images
{
	internal static class Images
	{
		public static readonly BitmapImage Breakpoint = LoadBitmap("Breakpoint");
		public static readonly BitmapImage CurrentLine = LoadBitmap("CurrentLine");

		public static readonly BitmapImage ViewCode = LoadBitmap("ViewCode");
		public static readonly BitmapImage Save = LoadBitmap("SaveFile");
		public static readonly BitmapImage OK = LoadBitmap("OK");

		public static readonly BitmapImage Delete = LoadBitmap("Delete");
		public static readonly BitmapImage Search = LoadBitmap("Search");

		public static readonly BitmapImage Assembly = LoadBitmap("Assembly");
		public static readonly BitmapImage AssemblyWarning = LoadBitmap("AssemblyWarning");
		public static readonly BitmapImage AssemblyLoading = LoadBitmap("FindAssembly");

		public static readonly BitmapImage Library = LoadBitmap("Library");
		public static readonly BitmapImage Namespace = LoadBitmap("NameSpace");

		public static readonly BitmapImage ReferenceFolderOpen = LoadBitmap("ReferenceFolder.Open");
		public static readonly BitmapImage ReferenceFolderClosed = LoadBitmap("ReferenceFolder.Closed");

		public static readonly BitmapImage SubTypes = LoadBitmap("SubTypes");
		public static readonly BitmapImage SuperTypes = LoadBitmap("SuperTypes");

		public static readonly BitmapImage FolderOpen = LoadBitmap("Folder.Open");
		public static readonly BitmapImage FolderClosed = LoadBitmap("Folder.Closed");

		public static readonly BitmapImage Resource = LoadBitmap("Resource");
		public static readonly BitmapImage ResourceImage = LoadBitmap("ResourceImage");
		public static readonly BitmapImage ResourceResourcesFile = LoadBitmap("ResourceResourcesFile");
		public static readonly BitmapImage ResourceXml = LoadBitmap("ResourceXml");
		public static readonly BitmapImage ResourceXsd = LoadBitmap("ResourceXsd");
		public static readonly BitmapImage ResourceXslt = LoadBitmap("ResourceXslt");

		public static readonly BitmapImage Class = LoadBitmap("Class");
		public static readonly BitmapImage Struct = LoadBitmap("Struct");
		public static readonly BitmapImage Interface = LoadBitmap("Interface");
		public static readonly BitmapImage Delegate = LoadBitmap("Delegate");
		public static readonly BitmapImage Enum = LoadBitmap("Enum");
		public static readonly BitmapImage StaticClass = LoadBitmap("StaticClass");


		public static readonly BitmapImage Field = LoadBitmap("Field");
		public static readonly BitmapImage FieldReadOnly = LoadBitmap("FieldReadOnly");
		public static readonly BitmapImage Literal = LoadBitmap("Literal");
		public static readonly BitmapImage EnumValue = LoadBitmap("EnumValue");

		public static readonly BitmapImage Method = LoadBitmap("Method");
		public static readonly BitmapImage Constructor = LoadBitmap("Constructor");
		public static readonly BitmapImage VirtualMethod = LoadBitmap("VirtualMethod");
		public static readonly BitmapImage Operator = LoadBitmap("Operator");
		public static readonly BitmapImage ExtensionMethod = LoadBitmap("ExtensionMethod");
		public static readonly BitmapImage PInvokeMethod = LoadBitmap("PInvokeMethod");

		public static readonly BitmapImage Property = LoadBitmap("Property");
		public static readonly BitmapImage Indexer = LoadBitmap("Indexer");

		public static readonly BitmapImage Event = LoadBitmap("Event");

		private static readonly BitmapImage OverlayProtected = LoadBitmap("OverlayProtected");
		private static readonly BitmapImage OverlayInternal = LoadBitmap("OverlayInternal");
		private static readonly BitmapImage OverlayProtectedInternal = LoadBitmap("OverlayProtectedInternal");
		private static readonly BitmapImage OverlayPrivate = LoadBitmap("OverlayPrivate");

		private static readonly BitmapImage OverlayStatic = LoadBitmap("OverlayStatic");


		private static readonly TypeIconCache typeIconCache = new TypeIconCache();
		private static readonly MemberIconCache memberIconCache = new MemberIconCache();

		private static BitmapImage LoadBitmap(string name)
		{
			// pack://application:,,,/ReferencedAssembly;component/ResourceFile.xaml
			var image =
				new BitmapImage(new Uri("pack://application:,,,/AssemblyVisualizer.Plugin;component/Images/" + name + ".png"));
			image.Freeze();
			return image;
		}

		public static BitmapImage LoadImage(object part, string icon)
		{
			Uri uri;
			var assembly = part.GetType().Assembly;
			if (assembly == typeof(Images).Assembly)
			{
				uri = new Uri("pack://application:,,,/" + icon);
			}
			else
			{
				var name = assembly.GetName();
				uri = new Uri("pack://application:,,,/" + name.Name + ";v" + name.Version + ";component/" + icon);
			}
			var image = new BitmapImage(uri);
			image.Freeze();
			return image;
		}

		public static ImageSource GetIcon(TypeIcon icon, AccessOverlayIcon overlay)
		{
			lock (typeIconCache)
			{
				return typeIconCache.GetIcon(icon, overlay, false);
			}
		}

		public static ImageSource GetIcon(MemberIcon icon, AccessOverlayIcon overlay, bool isStatic)
		{
			lock (memberIconCache)
			{
				return memberIconCache.GetIcon(icon, overlay, isStatic);
			}
		}

		public static ImageSource GetTypeIcon(TypeInfo type)
		{
			TypeIcon typeIcon;

			if (type.IsValueType)
			{
				if (type.IsEnum)
					typeIcon = TypeIcon.Enum;
				else
					typeIcon = TypeIcon.Struct;
			}
			else
			{
				if (type.IsInterface)
					typeIcon = TypeIcon.Interface;
				else if (IsDelegate(type))
					typeIcon = TypeIcon.Delegate;
				else if (IsStaticClass(type))
					typeIcon = TypeIcon.StaticClass;
				else
					typeIcon = TypeIcon.Class;
			}

			var overlayIcon = GetOverlayIcon(type);

			return GetIcon(typeIcon, overlayIcon);
		}

		private static bool IsDelegate(TypeInfo type)
		{
			return type.BaseType != null && type.BaseType.FullName == typeof(MulticastDelegate).FullName;
		}

		private static bool IsStaticClass(TypeInfo type)
		{
			return type.IsSealed && type.IsAbstract;
		}

		public static ImageSource GetMethodIcon(MethodInfo method)
		{
			if (method.IsSpecialName && method.Name.StartsWith("op_", StringComparison.Ordinal))
				return GetIcon(MemberIcon.Operator, GetOverlayIcon(method), false);

			/*if (method.IsStatic && method.HasCustomAttributes)
		    {
		        foreach (var ca in method.CustomAttributes)
		        {
		            if (ca.AttributeType.FullName == "System.Runtime.CompilerServices.ExtensionAttribute")
		            {
		                return Images.GetIcon(MemberIcon.ExtensionMethod, GetOverlayIcon(method.Attributes), false);
		            }
		        }
		    }*/

			if (method.IsSpecialName &&
			    (method.Name == ".ctor" || method.Name == ".cctor"))
				return GetIcon(MemberIcon.Constructor, GetOverlayIcon(method), false);

			/*if (method.HasPInvokeInfo)
		        return Images.GetIcon(MemberIcon.PInvokeMethod, GetOverlayIcon(method.Attributes), true);*/

			var showAsVirtual = method.IsVirtual && !(!method.IsOverride && method.IsFinal) && !method.DeclaringType.IsInterface;

			return GetIcon(
				showAsVirtual ? MemberIcon.VirtualMethod : MemberIcon.Method,
				GetOverlayIcon(method),
				method.IsStatic);
		}

		public static ImageSource GetEventIcon(EventInfo eventInfo)
		{
			//MethodDefinition accessor = eventDef.AddMethod ?? eventDef.RemoveMethod;
			//if (accessor != null)
			return GetIcon(MemberIcon.Event, GetOverlayIcon(eventInfo), eventInfo.IsStatic);
			/*else
			    return Images.GetIcon(MemberIcon.Event, AccessOverlayIcon.Public, false);*/
		}

		public static ImageSource GetFieldIcon(FieldInfo field)
		{
			if (field.DeclaringType.IsEnum && !field.IsSpecialName)
				return GetIcon(MemberIcon.EnumValue, GetOverlayIcon(field), false);

			if (field.IsLiteral)
				return GetIcon(MemberIcon.Literal, GetOverlayIcon(field), false);
			if (field.IsInitOnly)
				return GetIcon(MemberIcon.FieldReadOnly, GetOverlayIcon(field), field.IsStatic);
			return GetIcon(MemberIcon.Field, GetOverlayIcon(field), field.IsStatic);
		}

		public static ImageSource GetPropertyIcon(PropertyInfo property, bool isIndexer = false)
		{
			var icon = isIndexer ? MemberIcon.Indexer : MemberIcon.Property;
			return GetIcon(icon, GetOverlayIcon(property), property.IsStatic);
		}

		private static AccessOverlayIcon GetOverlayIcon(MemberInfo memberInfo)
		{
			if (memberInfo.IsPublic)
				return AccessOverlayIcon.Public;
			if (memberInfo.IsInternal || memberInfo.IsProtectedAndInternal)
				return AccessOverlayIcon.Internal;
			if (memberInfo.IsProtected || memberInfo.IsProtectedOrInternal)
				return AccessOverlayIcon.Protected;
			if (memberInfo.IsPrivate)
				return AccessOverlayIcon.Private;
			throw new NotSupportedException();
		}

		#region icon caches & overlay management

		private class TypeIconCache : IconCache<TypeIcon>
		{
			public TypeIconCache()
			{
				PreloadPublicIconToCache(TypeIcon.Class, Class);
				PreloadPublicIconToCache(TypeIcon.Enum, Enum);
				PreloadPublicIconToCache(TypeIcon.Struct, Struct);
				PreloadPublicIconToCache(TypeIcon.Interface, Interface);
				PreloadPublicIconToCache(TypeIcon.Delegate, Delegate);
				PreloadPublicIconToCache(TypeIcon.StaticClass, StaticClass);
			}

			protected override ImageSource GetBaseImage(TypeIcon icon)
			{
				ImageSource baseImage;
				switch (icon)
				{
					case TypeIcon.Class:
						baseImage = Class;
						break;
					case TypeIcon.Enum:
						baseImage = Enum;
						break;
					case TypeIcon.Struct:
						baseImage = Struct;
						break;
					case TypeIcon.Interface:
						baseImage = Interface;
						break;
					case TypeIcon.Delegate:
						baseImage = Delegate;
						break;
					case TypeIcon.StaticClass:
						baseImage = StaticClass;
						break;
					default:
						throw new NotSupportedException();
				}

				return baseImage;
			}
		}

		private class MemberIconCache : IconCache<MemberIcon>
		{
			public MemberIconCache()
			{
				PreloadPublicIconToCache(MemberIcon.Field, Field);
				PreloadPublicIconToCache(MemberIcon.FieldReadOnly, FieldReadOnly);
				PreloadPublicIconToCache(MemberIcon.Literal, Literal);
				PreloadPublicIconToCache(MemberIcon.EnumValue, EnumValue);
				PreloadPublicIconToCache(MemberIcon.Property, Property);
				PreloadPublicIconToCache(MemberIcon.Indexer, Indexer);
				PreloadPublicIconToCache(MemberIcon.Method, Method);
				PreloadPublicIconToCache(MemberIcon.Constructor, Constructor);
				PreloadPublicIconToCache(MemberIcon.VirtualMethod, VirtualMethod);
				PreloadPublicIconToCache(MemberIcon.Operator, Operator);
				PreloadPublicIconToCache(MemberIcon.ExtensionMethod, ExtensionMethod);
				PreloadPublicIconToCache(MemberIcon.PInvokeMethod, PInvokeMethod);
				PreloadPublicIconToCache(MemberIcon.Event, Event);
			}

			protected override ImageSource GetBaseImage(MemberIcon icon)
			{
				ImageSource baseImage;
				switch (icon)
				{
					case MemberIcon.Field:
						baseImage = Field;
						break;
					case MemberIcon.FieldReadOnly:
						baseImage = FieldReadOnly;
						break;
					case MemberIcon.Literal:
						baseImage = Literal;
						break;
					case MemberIcon.EnumValue:
						baseImage = Literal;
						break;
					case MemberIcon.Property:
						baseImage = Property;
						break;
					case MemberIcon.Indexer:
						baseImage = Indexer;
						break;
					case MemberIcon.Method:
						baseImage = Method;
						break;
					case MemberIcon.Constructor:
						baseImage = Constructor;
						break;
					case MemberIcon.VirtualMethod:
						baseImage = VirtualMethod;
						break;
					case MemberIcon.Operator:
						baseImage = Operator;
						break;
					case MemberIcon.ExtensionMethod:
						baseImage = ExtensionMethod;
						break;
					case MemberIcon.PInvokeMethod:
						baseImage = PInvokeMethod;
						break;
					case MemberIcon.Event:
						baseImage = Event;
						break;
					default:
						throw new NotSupportedException();
				}

				return baseImage;
			}
		}

		private abstract class IconCache<T>
		{
			private static readonly Rect iconRect = new Rect(0, 0, 16, 16);

			private readonly Dictionary<Tuple<T, AccessOverlayIcon, bool>, ImageSource> cache =
				new Dictionary<Tuple<T, AccessOverlayIcon, bool>, ImageSource>();

			protected void PreloadPublicIconToCache(T icon, ImageSource image)
			{
				var iconKey = new Tuple<T, AccessOverlayIcon, bool>(icon, AccessOverlayIcon.Public, false);
				cache.Add(iconKey, image);
			}

			public ImageSource GetIcon(T icon, AccessOverlayIcon overlay, bool isStatic)
			{
				var iconKey = new Tuple<T, AccessOverlayIcon, bool>(icon, overlay, isStatic);
				if (cache.ContainsKey(iconKey))
				{
					return cache[iconKey];
				}
				var result = BuildMemberIcon(icon, overlay, isStatic);
				cache.Add(iconKey, result);
				return result;
			}

			private ImageSource BuildMemberIcon(T icon, AccessOverlayIcon overlay, bool isStatic)
			{
				var baseImage = GetBaseImage(icon);
				var overlayImage = GetOverlayImage(overlay);

				return CreateOverlayImage(baseImage, overlayImage, isStatic);
			}

			protected abstract ImageSource GetBaseImage(T icon);

			private static ImageSource GetOverlayImage(AccessOverlayIcon overlay)
			{
				ImageSource overlayImage;
				switch (overlay)
				{
					case AccessOverlayIcon.Public:
						overlayImage = null;
						break;
					case AccessOverlayIcon.Protected:
						overlayImage = OverlayProtected;
						break;
					case AccessOverlayIcon.Internal:
						overlayImage = OverlayInternal;
						break;
					case AccessOverlayIcon.ProtectedInternal:
						overlayImage = OverlayProtectedInternal;
						break;
					case AccessOverlayIcon.Private:
						overlayImage = OverlayPrivate;
						break;
					default:
						throw new NotSupportedException();
				}
				return overlayImage;
			}

			private static ImageSource CreateOverlayImage(ImageSource baseImage, ImageSource overlay, bool isStatic)
			{
				var group = new DrawingGroup();

				group.Children.Add(new ImageDrawing(baseImage, iconRect));

				if (overlay != null)
					group.Children.Add(new ImageDrawing(overlay, iconRect));

				if (isStatic)
					group.Children.Add(new ImageDrawing(OverlayStatic, iconRect));

				var image = new DrawingImage(group);
				image.Freeze();
				return image;
			}
		}

		#endregion
	}
}