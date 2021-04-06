import React from 'react';
import {useHistory} from "react-router-dom";
import {keyTitle} from "../helpers";
import ArrowForwardIosIcon from '@material-ui/icons/ArrowForwardIos';
import {Grid} from "@material-ui/core";


const renderCrumbs = (
  crumbs: string[],
  crumbComponent: (crumb: string, isEven: number) => JSX.Element
) => crumbs.map((crumb: string, index) => crumbComponent(crumb, index));

const BreadCrumbs = ({styles, crumbs}: any) => {
  const history = useHistory();
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
      {((index+1) < crumbs.length) && <ArrowForwardIosIcon
        className={styles.paleFont}
        style={{fontSize: '0.8em'}}
      />}
    </>
  }</React.Fragment>

  if (!crumbs.length) return <span>Where crumbs?</span>;

  return <Grid container justify={'flex-start'} alignItems={'center'} style={{gap: '10px'}}>
    {renderCrumbs(crumbs, crumbComponent)}
  </Grid>;

}

export default BreadCrumbs;
