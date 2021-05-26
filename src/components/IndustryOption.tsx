import React from 'react';
import {connect} from "react-redux";
import {RootState} from "../store/rootReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../store/actionType";
import {LookupSearchActionType} from "./lookupSearch/duck";
import {useIndustruOptionStyles} from "./customSearch/useStyles";
import {Grid} from "@material-ui/core";
import {StyledCheckbox} from "./checkbox";
import {renderOptions, IndustryOptionType} from "../pages/details/helpers";


interface IndustryOptionProps {
  industry: IndustryOptionType,
  options: number[],
  saveIndustryOptions: (checked: boolean, id: number) => void,
}

const IndustryOption = ({
     industry: {id, name, children},
     saveIndustryOptions, options,
   }: IndustryOptionProps) => {
  const styles = useIndustruOptionStyles();

  const renderRow = (name: string) => <Grid
    wrap={'nowrap'}
    container
    alignItems={'center'}
    className={!children ? styles.marginLeft15 : styles.parent}
  >
    <div>
      <StyledCheckbox
        onChange={(e) => saveIndustryOptions(e.target.checked, id)}
        checked={options.indexOf(id) > -1}
      />
    </div>
    <div><span>{name}</span></div>
  </Grid>

  return <>
    {renderRow(name)}
    {children && renderOptions(children)}
  </>
};


export default connect(
  (state: RootState) => ({
    options: state.LookupSearch.data.options,
  }),
  (dispatch: Dispatch) => ({
    saveIndustryOptions: (checked: boolean, id: number) =>
      dispatch(CreateAction(LookupSearchActionType.SAVE_INDUSTRY_OPTIONS, {checked, id})),
  })
)(IndustryOption);
