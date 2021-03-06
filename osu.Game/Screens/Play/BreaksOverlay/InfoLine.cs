﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Screens.Play.BreaksOverlay
{
    public class InfoLine<T> : Container
        where T : struct
    {
        private const int margin = 2;

        public Bindable<T> Current = new Bindable<T>();

        private readonly OsuSpriteText text;
        private readonly OsuSpriteText valueText;

        private readonly string prefix;

        public InfoLine(string name, string prefix = @"")
        {
            this.prefix = prefix;

            AutoSizeAxes = Axes.Y;
            Children = new Drawable[]
            {
                text = new OsuSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.CentreRight,
                    Text = name,
                    TextSize = 17,
                    Margin = new MarginPadding { Right = margin }
                },
                valueText = new OsuSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.CentreLeft,
                    Text = prefix + @"-",
                    TextSize = 17,
                    Font = "Exo2.0-Bold",
                    Margin = new MarginPadding { Left = margin }
                }
            };

            Current.ValueChanged += currentValueChanged;
        }

        private void currentValueChanged(T newValue)
        {
            var newText = prefix + Format(newValue);

            if (valueText.Text == newText)
                return;

            valueText.Text = newText;
        }

        protected virtual string Format(T count) => count.ToString();

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            text.Colour = colours.Yellow;
            valueText.Colour = colours.YellowLight;
        }
    }

    public class PercentageInfoLine : InfoLine<double>
    {
        public PercentageInfoLine(string name, string prefix = "") : base(name, prefix)
        {
        }

        protected override string Format(double count) => $@"{count:P2}";
    }
}
