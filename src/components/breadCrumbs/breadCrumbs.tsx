import React from 'react';
import {useHistory} from "react-router-dom";
import {keyTitle} from "../../helpers";
import {Grid} from "@material-ui/core";
import useStyles from "./useStyles";
import Arrow from "../Arrow";


const renderCrumbs = (
  crumbs: string[],
  crumbComponent: (crumb: string, isEven: number) => JSX.Element
) => crumbs.map((crumb: string, index) => crumbComponent(crumb, index));

const BreadCrumbs = ({crumbs}: any) => {
  const history = useHistory();
  const styles = useStyles();

  const crumbComponent = (crumb: string, index: number) => <React.Fragment key={`/${crumb}`}>{
     // TODO: solutions for styles
    crumb && <>
      <span
        key={`/${crumb}`}
        onClick={() => history.push(`/${crumb}`)}
        className={`${styles.purpColor} ${styles.link}`}
        style={{ width: index === 0 ? '105px' : 'auto' }}
      >
        {keyTitle(crumb)}
      </span>
      {((index+1) < crumbs.length) && <Arrow direction={'right'} />}
    </>
  }</React.Fragment>

  if (!crumbs.length) return <span>Where crumbs?</span>;

  return <Grid container justify={'flex-start'} alignItems={'center'} style={{gap: '5px'}}>
    {renderCrumbs(crumbs, crumbComponent)}
  </Grid>;

}

export default BreadCrumbs;
