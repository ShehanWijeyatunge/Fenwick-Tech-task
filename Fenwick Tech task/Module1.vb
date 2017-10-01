'Author Shehan Wijeyatunge
'Program Fenwick Technical Test
Imports System
Imports System.IO
Module Module1
    ' New Line value for ease of use
    Dim nl As String = Environment.NewLine


    Dim UserVals As String
    ' Stored values to be used in summary asnd saving

    Sub Main()
        'Start of Main Routine
        'Handles Input and format of user input
        Dim action As String = ""
        Dim fileName As String = ""
        Dim vals As String = ""

        'Loop until "Quit" is selected
        Dim bQuit As Boolean = False
        Do
            'greeting message
            Console.WriteLine("*******************************************" + nl + "Hello There What would you like to do today")
            'Capture user input
            Dim input = Console.ReadLine()

            'Split input to locate action paramteres 
            Dim inputArray = Split(input)

            'used to determin hhow many parameters are used
            Dim upper As Int16 = inputArray.GetUpperBound(0)

            'Action user wants to perform
            'forced lowercase for ease of use
            action = inputArray(0).ToLower()

            'check if array input contains appropriate values
            If upper > 0 Then
                'Filename for file saving and retrieval
                fileName = inputArray(1)
                If upper > 1 Then
                    'Vals to be used in calculations
                    vals = inputArray(2)
                End If
            End If

            Select Case action

                Case "record"
                    Call Record(fileName, vals)
                Case "summary"
                    Call Summary(fileName)
                Case "help"
                    Call Help()
                Case "quit"
                    ' exit program
                    Console.WriteLine("Exiting Program" + nl)

                    bQuit = True
                Case Else
                    InputError()
            End Select

        Loop Until bQuit

    End Sub

    Sub Record(filename As String, vals As String)
        'Saves data into specified filename, path is based on the locations of program
        ' check data is numeric
        If IsNumeric(vals) Then
            'locate Program location
            Dim appDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
            Dim filePath = System.IO.Path.Combine(appDir, filename)
            'save vals based on filepath entered
            System.IO.File.WriteAllText(filePath, vals)
        Else
            Console.WriteLine("Values entered must be numeric, see help for more information" + nl)
        End If

    End Sub

    Sub Summary(filename As String)

        'values to be used in calculations
        Dim dEntries As Double
        Dim iMin As Int32
        Dim iMax As Int32
        Dim iAvg As Int32


        'Values to be used in the output strings
        Dim sEntries As String
        Dim sMin As String
        Dim sMax As String
        Dim sAvg As String

        'used for error handling
        Dim bfileReadError As Boolean

        'Find Program location
        Dim appDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        Dim filePath = System.IO.Path.Combine(appDir, filename)

        If File.Exists(filePath) Then

            'Read File based on program location and filename input from user
            Dim vals() As String = File.ReadAllLines(filename)

            Dim valsArray() As String = Split(vals(0), ",")

            'calculate sum of all values for average
            Dim valsSum = 0.0
            For Each n In valsArray
                'confirm data integrity
                If IsNumeric(n) Then
                    valsSum += Integer.Parse(n)
                Else
                    Console.WriteLine(" " + n + " ")
                    bfileReadError = True
                End If

            Next

            If bfileReadError = False Then

                'find total entries
                dEntries = valsArray.Length
                'find average value
                iAvg = valsSum / dEntries
                'find max value
                iMax = valsArray.Max
                'find minimum value
                iMin = valsArray.Min

                'build output data
                sEntries = "| # of Entries |  " + dEntries.ToString + nl
                sMin = "| Min. value   |  " + iMin.ToString + nl
                sMax = "| Max. value   |  " + iMax.ToString + nl
                sAvg = "| Avg. value   |  " + iAvg.ToString + nl

                Console.WriteLine("+--------------+------+" + nl + sEntries + sMin + sMax + sAvg + "+--------------+------+")
            Else
                'data not all numeric failure handling
                Console.WriteLine("One or more values is not Numeric please verify data integrity")

            End If

        Else
            'cannot find file error handling
            Console.WriteLine("ERROR cannot find file. Please confirm it exists and try again")
        End If

    End Sub

    Sub Help()
        'Help Action
        Dim sHelpIntro As String
        Dim sHelpRecord As String
        Dim sHelpSummary As String

        'help string as build then displayed in one action
        sHelpIntro = "This Program is for Recording numeric values into a file, reading that file and then summarizing them."
        sHelpRecord = "Record [filename] [values]   Record will save all values you enter in delimitered by ',' into the your enter filename. The file is save where this programed is located."
        sHelpSummary = "Summary [filename]  Summary will read specified file and display a summarized output. The available outputs are: Number of values entered,Aaverage, Maximum value, Minumum Value."
        Console.WriteLine(sHelpIntro + nl + "Available Actions: Record, Summary and Help" + nl + sHelpRecord + nl + sHelpSummary + nl)



    End Sub

    Sub InputError()

        Console.WriteLine(nl + "Invalid Action Please see help for list of available actions" + nl)
    End Sub


End Module
