﻿/*
 * Copyright © 2012-2016 VMware, Inc.  All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the “License”); you may not
 * use this file except in compliance with the License.  You may obtain a copy
 * of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an “AS IS” BASIS, without
 * warranties or conditions of any kind, EITHER EXPRESS OR IMPLIED.  See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;
using AppKit;
using VMDirSchemaEditorSnapIn.Nodes;
using System.Collections.Generic;
using Foundation;
using VMDirSchema;
using System.Linq;
using VMDir.Common.Schema;

namespace VMDirSchemaEditorSnapIn.ListViews
{
    public class ObjectClassesListView : NSTableViewDataSource
    {
        public  List<VMDirSchemaClassEntryNode> Entries { get; set; }

        public ObjectClassesListView(VMDirSchemaClassBaseNode node)
        {
            if (node != null)
                Entries = node.Children.OfType<VMDirSchemaClassEntryNode>().ToList();
        }

        // This method will be called by the NSTableView control to learn the number of rows to display.
        [Export("numberOfRowsInTableView:")]
        public int NumberOfRowsInTableView(NSTableView table)
        {
            return (Entries != null) ? Entries.Count : 0;
        }

        // This method will be called by the control for each column and each row.
        [Export("tableView:objectValueForTableColumn:row:")]
        public NSObject ObjectValueForTableColumn(NSTableView table, NSTableColumn col, int row)
        {
            NSString value = NSString.Empty;
            try
            {
                if (Entries != null)
                {
                    VMDirSchemaClassEntryNode record = Entries[row];
                    switch (col.Identifier)
                    {
                        case VMDirSchemaConstants.VMDIRSCHEMA_NAME:
                            value = (NSString)record.DisplayName;
                            break;
                        case VMDirSchemaConstants.VMDIRSCHEMA_CLASS_TYPE:
                            value = (NSString)record.ObjectClassDto.GetObjectClassTypeAsString();
                            break;
                        case VMDirSchemaConstants.VMDIRSCHEMA_DESC:
                            value = (NSString)record.ObjectClassDto.Description;
                            break;
                    }
                }
            }
            catch (Exception)
            {
                value = NSString.Empty;
            }
            return value;
        }
    }
}

