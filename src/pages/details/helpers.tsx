import {Grid} from "@material-ui/core";
import {keyTitle} from "../../helpers";
import {paintRatingClass} from "../results/prepareForGrid";
import React from "react";
import {paintRating} from "../../components/charts/useStyles";
import IndustryOption from "../../components/IndustryOption";

// TODO: actually might be refactored into component

const renderValue = (value: string | number | boolean) => <span>
    {typeof value === "boolean" ?
      value ? 'Yes' : 'No'
      : value}
  </span>

export const renderSingleProp = (key: string, value: string | number) => <Grid item key={`${value}${key}`}>
  <Grid direction={'column'} container>
    <span>{keyTitle(key)}</span>
    {
      (key === 'Rating' || key === 'Average score') ? <Grid container direction={'row'} alignItems={'center'}>
          <div
            className={typeof value === 'string' ? paintRatingClass(value) : ''}
            style={{
              background: typeof value === 'number' ? paintRating(value) : '',
              width: '10px',
              height: '10px',
              borderRadius: '50%',
              marginRight: '5px',
            }}
          > </div>
          {renderValue(value)}
        </Grid>
        :
        <>{renderValue(value)}</>
    }
  </Grid>
</Grid>;


export type OptionType = {
  id: number,
  name: string,
  children?: OptionType[],
}

export const renderOptions = (
  industries: OptionType[],
  renderIcon?: () => JSX.Element,
  handleClick?: (id: number) => void,
) =>
  industries.map(industry => <IndustryOption
    key={industry.id}
    industry={industry}
    renderIcon={renderIcon}
    handleClick={handleClick}
  />);