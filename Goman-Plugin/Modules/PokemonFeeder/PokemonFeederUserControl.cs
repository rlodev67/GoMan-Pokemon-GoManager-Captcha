﻿using System;
using System.IO;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Goman_Plugin.Model;
using GoPlugin;
using GoPlugin.Extensions;

namespace Goman_Plugin.Modules.PokemonFeeder
{
    public partial class PokemonFeederUserControl : UserControl
    {
        public PokemonFeederUserControl()
        {
            InitializeComponent();

            this.fastObjectListViewLogs.PrimarySortColumn = this.olvColumnDate;
            this.fastObjectListViewLogs.PrimarySortOrder = SortOrder.Descending;
            this.fastObjectListViewLogs.ListFilter = new TailFilter(200);
        }

        internal void SetControls()
        {
            cbkEnabled.Checked = Plugin.PokemonFeederModule.Settings.Enabled;
            fastObjectListViewLogs.SetObjects(Plugin.PokemonFeederModule.Logs);

           // Plugin.PokemonFeederModule.LogEvent += (o, model) => fastObjectListViewLogs.AddObject(model);
        }

        private async void cbkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Plugin.PokemonFeederModule.Settings.Enabled = cbkEnabled.Checked;
            await Plugin.PokemonFeederModule.SaveSettings();
            if (!Plugin.PokemonFeederModule.Settings.Enabled)
                await Plugin.PokemonFeederModule.Disable(true);
            else if(!Plugin.PokemonFeederModule.IsEnabled)
                await Plugin.PokemonFeederModule.Enable(true);
        }

        private void fastObjectListViewLogs_FormatCell(object sender, FormatCellEventArgs e)
        {
            LogModel log = e.Model as LogModel;
            if (log != null)
            {
                e.SubItem.ForeColor = log.GetLogColor();
            }
        }
    }
}
