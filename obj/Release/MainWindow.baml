   M S B A M L   `   `   `  ����   
NITHtester    NITHtester.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    >d2http://schemas.microsoft.com/expression/blend/2008    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � Title$  
NITHtester�� � WindowStyle$ SingleBorderWindow=� � 
ResizeMode$ 	CanResize=� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�$
��1500��$
��1600��.��� ���#222222.+��� $��0,0,0,0q�.���� $��10,10,10,10q����#222222 �� Orientation$ Vertical=�.+��� $ 
Horizontal=�.+��� $��0��$��0��$��Sensor port:��$
��Left=�   
EntryLabel#��[  �� $��0��$��1��$ 
Horizontal=�$
��Left=�$��30��.+��� -    ��Name$ btnSensorPortMinus��$��-��$��40��$
��Left=���   BlackBorder#��[ $��40��$��5,0,5,0q�._��� -   $��txtSensorPort��  	WhiteText#��[ $��40�� �� TextAlignment$ Center=�$��Center=�$��Center=�$
��Bold)��� -   $ btnSensorPortPlus��$��+��$��40��$
��Left=��� -   $ 
btnConnect��$��Connect��$��80��$��40,0,0,0q�A� -    A�Name$ indConnection��$��20��$��20��
�������$��2��
���  ��$��10,0,0,0q�	 J� Clip	 @� 
 @� RadiusX$
 40Q� @� RadiusY$ 40Q� @� Center$ 20,20&��� -   $ btnPause��$��Pause��$��80��$��40,0,0,0q�]� ��. .+��� #��[  $��Sensor name:��$
��Left=��� #��[ ._��� -    ��Name$ txtSensorName��#��[ ]� ��. .+��� #��[  $��Sensor version:��$
��Left=��� #��[ ._��� -	   $��txtSensorVersion��#��[ $
��Left=�]� ��. .+��� #��[  $��Status code:��$
��Left=��� #��[ ._��� -
   $��txtStatusCode��#��[ $
��Left=�]� ��. .+��� #��[  $��Extra data (optional):��$
��Left=��� #��[ ._��� -   $��txtExtraData��#��[ $
��Left=�]� ��. .+��� #��[  $��Arguments and values:��$
��Left=��� #��[ ._��� -   $��txtArgumentsValues��  WhiteTextBlackBox#��[ $	��300��$
��Left=� �� TextWrapping$
 Wrap=� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�]� ��. .+��� #��[  $��Errors list:��$
��Left=��� #��[ ._��� -   $��	txtErrors��#��[ $	��100��$
��Left=�$
 Wrap=� . $
 Auto=�]� ��. .+��� #��[  $��Raw strings:��$
��Left=��� #��[ ._��� -   $��txtRawStrings��#��[ $	��100��$
��Left=�$
 Wrap=� . $
 Auto=��� $ Vertical=�.+��� $ 
Horizontal=�.+��� #��[  $��Bindable gauge 1:��$
��Left=�� -   $��	prbGauge1��
���   �
�������$��0q� � Orientation$ 
Horizontal=� � Maximum$	 100Q� � Minimum$ 0Q�$	��360���� #��[  $
��Arg:��$��50��$
��Left=�$��Center=��� -   $��	lstGauge1��$	��100���� #��[  $
��Min:��$��50��$
��Left=�$��Center=��� -   $��txtMinGauge1��#��[ $��50��$
��Nope���� #��[  $
��Max:��$��50��$
��Left=�$��Center=��� -   $��txtMaxGauge1��#��[ $��50���� #��[  $��Offset:��$��50��$
��Left=�$��Center=��� -   $��txtOffGauge1��#��[ $��50���� #��[  $��Prop.:��$��50��$
��Left=�$��Center=��� -   $��chkProp1��$��Center=�$
��Auto���� $ 
Horizontal=�.+��� #��[  $��Bindable gauge 2:��$
��Left=�� -   $��	prbGauge2��
���   �
�������$��0q�$ 
Horizontal=�$	 100Q�$ 0Q�$	��360���� #��[  $
��Arg:��$��50��$
��Left=�$��Center=��� -   $��	lstGauge2��$	��100���� #��[  $
��Min:��$��50��$
��Left=�$��Center=��� -   $��txtMinGauge2��#��[ $��50���� #��[  $
��Max:��$��50��$
��Left=�$��Center=��� -   $��txtMaxGauge2��#��[ $��50���� #��[  $��Offset:��$��50��$
��Left=�$��Center=��� -   $��txtOffGauge2��#��[ $��50���� #��[  $��Prop.:��$��50��$
��Left=�$��Center=��� -   $��chkProp2��$��Center=�$
��Auto���� $ 
Horizontal=�.+��� #��[  $��Bindable gauge 3:��$
��Left=�� -   $��	prbGauge3��
���   �
�������$��0q�$ 
Horizontal=�$	 100Q�$ 0Q�$	��360���� #��[  $
��Arg:��$��50��$
��Left=�$��Center=��� -   $��	lstGauge3��$	��100���� #��[  $
��Min:��$��50��$
��Left=�$��Center=��� -   $��txtMinGauge3��#��[ $��50���� #��[  $
��Max:��$��50��$
��Left=�$��Center=��� -   $��txtMaxGauge3��#��[ $��50���� #��[  $��Offset:��$��50��$
��Left=�$��Center=��� -   $��txtOffGauge3��#��[ $��50���� #��[  $��Prop.:��$��50��$
��Left=�$��Center=��� -    $��chkProp3��$��Center=�$
��Auto���� $ 
Horizontal=�.+��� #��[  $��Bindable gauge 4:��$
��Left=�� -!   $��	prbGauge4��
���   �
�������$��0q�$ 
Horizontal=�$	 100Q�$ 0Q�$	��360���� #��[  $
��Arg:��$��50��$
��Left=�$��Center=��� -"   $��	lstGauge4��$	��100���� #��[  $
��Min:��$��50��$
��Left=�$��Center=��� -#   $��txtMinGauge4��#��[ $��50���� #��[  $
��Max:��$��50��$
��Left=�$��Center=��� -$   $��txtMaxGauge4��#��[ $��50���� #��[  $��Offset:��$��50��$
��Left=�$��Center=��� -%   $��txtOffGauge4��#��[ $��50���� #��[  $��Prop.:��$��50��$
��Left=�$��Center=��� -&   $��chkProp4��$��Center=�$
��Auto���� $ 
Horizontal=�.+��� #��[  $��Bindable gauge 5:��$
��Left=�� -'   $��	prbGauge5��
���   �
�������$��0q�$ 
Horizontal=�$	 100Q�$ 0Q�$	��360���� #��[  $
��Arg:��$��50��$
��Left=�$��Center=��� -(   $��	lstGauge5��$	��100���� #��[  $
��Min:��$��50��$
��Left=�$��Center=��� -)   $��txtMinGauge5��#��[ $��50���� #��[  $
��Max:��$��50��$
��Left=�$��Center=��� -*   $��txtMaxGauge5��#��[ $��50���� #��[  $��Offset:��$��50��$
��Left=�$��Center=��� -+   $��txtOffGauge5��#��[ $��50���� #��[  $��Prop.:��$��50��$
��Left=�$��Center=��� -,   $��chkProp5��$��Center=�$
��Auto���� $ 
Horizontal=�.+��� #��[  $��HeadTracker tester:��$
��Left=��� --    ��Name$ cnvHeadTrackerPlot��
���   �$	��360��$	��360��.+�A� -.   $ dotPitchRoll��
��� � �$��15��$��15��A� -/   $ dotPitchYaw��
�������$��15��$��15���� $ Vertical=�.+��� $ 
Horizontal=�.+��� #��[  $��Enabled:��$	��100��$��10,0,0,0q�$
��Left=�$
��Left=��� -0   $��chkHTenabled��$��Center=�$
��Auto���� $ 
Horizontal=�.+��� #��[  $��Argument bind:��$	��100��$��10,0,0,0q�$
��Left=�$
��Left=��� -1   $��cbxHTargBind��$	��100���� $ 
Horizontal=�.+��� #��[  $��Yaw multiplier:��$	��100��$��10,0,0,0q�$
��Left=�$
��Left=��� -2   $��txtHTyawMultiplier��#��[ $	��100���� $ 
Horizontal=�.+��� #��[  $��Pitch multiplier:��$	��100��$��10,0,0,0q�$
��Left=�$
��Left=��� -3   $��txtHTpitchMultiplier��#��[ $	��100���� $ 
Horizontal=�.+��� #��[  $��Roll multiplier:��$	��100��$��10,0,0,0q�$
��Left=�$
��Left=��� -4   $��txtHTrollMultiplier��#��[ $	��100���� $ 
Horizontal=�.+��� #��[  $��
Calibrate:��$	��100��$��10,0,0,0q�$
��Left=�$
��Left=��� -5   $��btnHTcalibrate��$��
Set Center��$	��100���� $��50���� $ 
Horizontal=�.+��� #��[  $��	Raw Pos.:��$	��100��$��10,0,0,0q�$
��Left=�$
��Left=��� #��[ ._��� 2  ,System.Windows.Markup.XmlAttributeProperties  XmlSpace preserve-6   $��txtHTrawPosition��#��[ $
��Auto��$	��100��$��Center=�$
��Wrap=�$��Y:
P:
R:���� $ 
Horizontal=�.+��� #��[  $��Raw Accel.:��$	��100��$��10,0,0,0q�$
��Left=�$
��Left=��� #��[ ._���  preserve-7   $��txtHTrawAccel��#��[ $
��Auto��$	��100��$��Center=�$
��Wrap=�$��Y:
P:
R:��