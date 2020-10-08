import os
import sys
import clr
sys.path.append(os.path.dirname(os.path.abspath(__file__)) +'/bin/Release')
clr.AddReference("System.Windows.Forms")
clr.AddReference("MaterialControl")
import _thread as thread
from newsapi import NewsApiClient
from typing import Text, TextIO, overload
import System.Windows.Forms as WinForms
from MaterialControl import *
from System import String, DateTime, Object, EventHandler
from System.Collections import *
from System.Collections.Generic import List
from System.Drawing import Point, PointF, Size, SizeF, Color, SystemColors, Pen, Rectangle, RectangleF, SolidBrush, SystemBrushes, Graphics

news_api = NewsApiClient(api_key='f474d5ecd9d5452a8cf0b2adb1004871')
def search(self):
    query = self.layout['query'].Text
    src = self.layout['src'].Text
    cat = self.layout['cat'].Text
    frm = self.layout['from'].Text
    lang = self.layout['lang'].Text
    sort_by = self.layout['sort_by'].Text
    to_date = self.layout['to'].Text
    print(locals())
    pass
class MainWindow(WinForms.Form):

    def __init__(self):
        self.SuspendLayout()
        self.MinimumSize = self.MaximumSize = Size(1280, 720)
        self.Text = 'News Searcher'
        # self.AutoScaleMode = WinForms.AutoScaleMode.Dpi
        self.BackColor = MaterialColor.COLOR_SURFACE
        self.ForeColor = MaterialColor.COLOR_ON_SURFACE
        # prevent size change
        self.MaximizeBox = False
        self.MinimizeBox = False
        self.FormBorderStyle = WinForms.FormBorderStyle.FixedSingle
        # panel 'input'
        self.form = MaterialInputForm()
        self.form.Dock = WinForms.DockStyle.Top
        self.form.Size = Size(self.Width, int(self.Height * 0.3))
        self.layout = {
            'query': MaterialTextField('Query:'), 
            'src': MaterialTextField('Source:'), 
            'cat': MaterialTextField('Category:'),
            'from': MaterialDateTimePickerField('From:'),
            'lang': MaterialTextField('Language:'), 
            'sort_by': MaterialTextField('Sort by:'), 
            'to': MaterialDateTimePickerField('To:'),
            'col2': HSpacer(),
            'dummy': MaterialTextField('Dummy:'),
            'search_btn': MaterialButton('Search'),
            
        }
        self.layout['search_btn'].Click += EventHandler(lambda _, e: search(self))
        self.form.BeginUpdate()
        for control in self.layout.values():
            self.form.Controls.Add(control)
        self.form.EndUpdate()
        for key,value in self.layout.items():
            print('{} @ ({},{})'.format(key, value.Location.X, value.Location.Y))
        # news list
        self.news_list = MaterialListView()
        self.news_list.Location = Point(0, self.form.Height)
        self.news_list.Size = Size(self.Width, int(self.Height * .7))
        last = self.Width - (int(self.Width * .05) + int(self.Width * .15)
                             * 3 - WinForms.SystemInformation.VerticalScrollBarWidth)
        self.news_list.Columns.Add('Source', int(self.Width * .05))
        self.news_list.Columns.Add('Title', int(self.Width * .15))
        self.news_list.Columns.Add('Author', int(self.Width * .15))
        self.news_list.Columns.Add('Description', int(self.Width * .15))
        self.news_list.Columns.Add('Url', last)
        item = WinForms.ListViewItem('Test')
        item.SubItems.Add('Test')
        item.SubItems.Add('Test')
        item.SubItems.Add('Test')
        item.SubItems.Add('Test')
        self.news_list.Items.Add(item)
        self.Controls.Add(self.form)
        self.Controls.Add(self.news_list)
        self.ResumeLayout(False)
        self.PerformLayout()

def fetch_bbc(window):
    pass


def main():
    main_window = MainWindow()
    app = WinForms.Application
    thread.start_new_thread(fetch_bbc, (main_window,))
    app.Run(main_window)


    # print(page.text[page.text.find('promos:'):])
if __name__ == '__main__':
    main()
