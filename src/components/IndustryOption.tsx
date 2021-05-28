import React, {useState} from 'react';
import {connect} from "react-redux";
import {RootState} from "../store/rootReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../store/actionType";
import {LookupSearchActionType} from "./lookupSearch/duck";
import {useIndustruOptionStyles} from "./customSearch/useStyles";
import {Grid} from "@material-ui/core";
import {StyledCheckbox} from "./checkbox";
import {renderOptions, OptionType} from "../pages/details/helpers";


interface IndustryOptionProps {
  industry: OptionType,
  options: number[],
  saveIndustryOptions: (checked: boolean, id: number, actionType: LookupSearchActionType) => void,
  renderIcon?: () => JSX.Element,
  handleClick?: (id: number) => void,
}

const IndustryOption =
  ({
   industry: {id, name, children},
   saveIndustryOptions, options, renderIcon, handleClick
 }: IndustryOptionProps) => {
  const [selected, setSelected] = useState<boolean>(false);
  const styles = useIndustruOptionStyles();
  const tab = localStorage.getItem('tab');

  const renderRow = (
    id: number,
    name: string,
    handleClick?: (id: number) => void,
  ) => <Grid
    wrap={'nowrap'}
    container
    alignItems={'center'}
    className={`
      ${!children ? styles.marginLeft15 : styles.parent} 
      ${styles.root}
    `}
    onClick={() => {
      handleClick && handleClick(id);
      setSelected(!selected);
    }}
  >
    <div>
      <StyledCheckbox
        onChange={(e) => saveIndustryOptions(
          e.target.checked,
          id,
          LookupSearchActionType[tab === 'industry' ? 'SAVE_INDUSTRY_OPTIONS' : 'SAVE_COUNTRY_OPTIONS']
        )}
        checked={options.indexOf(id) > -1}
      />
    </div>
    <div><span>{name}</span></div>
    {renderIcon && renderIcon()}
  </Grid>

  return <>
    {renderRow(id, name, handleClick)}
    {children && renderOptions(children)}
  </>
};


export default connect(
  (state: RootState) => ({
    options: state.LookupSearch.data.options,
  }),
  (dispatch: Dispatch) => ({
    // todo: fix saving to localstorage. Now all ids being saved in same place so its causing not related
    //  ids participating in request
    saveIndustryOptions: (checked: boolean, id: number, actionType: LookupSearchActionType) =>
      dispatch(CreateAction(actionType, {checked, id})),
  })
)(IndustryOption);
