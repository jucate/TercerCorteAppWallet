using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using pkgWallet.pkgDomain;
using SQLitePCL;

namespace WebWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashBoardController : ControllerBase
    {
        private readonly clsDashBoard _clsDashBoard = clsDashBoard.opGetInstance();

        [HttpGet("GetWallet")]
        public IActionResult GetWallet(int prmOUID)
        {
            clsWallet wallet = _clsDashBoard.opGetWalletWith(prmOUID);
            if (wallet != null)
            {
                return Ok(wallet);
            }
            return BadRequest();
        }

        [HttpGet("GetCurrency")]
        public IActionResult GetCurrency(int prmOUID)
        {
            clsCurrency currency = _clsDashBoard.opGetCurrencyWith(prmOUID);
            if (currency != null)
            {
                return Ok(currency);
            }
            return BadRequest();
        }

        [HttpGet("GetPocket")]
        public IActionResult GetPocket(int prmOUIDWallet, int prmOUID)
        {
            clsWallet wallet = _clsDashBoard.opGetWalletWith(prmOUID);
            clsPocket pocket = wallet.opGetPocketWith(prmOUID);
            if (wallet != null)
            {
                if (pocket != null)
                {
                    return Ok(pocket);
                }
            }
            return BadRequest();
        }
        [HttpGet("GetMovement")]
        public IActionResult GetMovement(int prmOUIDWallet, int prmOUIDPocket, int prmOUID)
        {
            clsWallet wallet = _clsDashBoard.opGetWalletWith(prmOUID);
            clsPocket pocket = wallet.opGetPocketWith(prmOUID);
            clsMovement movement = pocket.opGetMovementWith(prmOUID);
            if (wallet != null)
            {
                if (pocket != null)
                {
                    if (movement != null)
                    {
                        return Ok(movement);
                    }
                }
            }
            return BadRequest();
        }
        [HttpPost("WriteDownWallet")]
        public IActionResult PostWallet(int prmOUID, string prmName, string prmClientName, string prmClienEmail)
        {
            _clsDashBoard.opClearMe();
            if (_clsDashBoard.opWriteDownWallet(prmOUID, prmName, prmClientName, prmClienEmail))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("WriteDownCurrency")]
        public IActionResult PostCurrency(int prmOUID, string prmName, double prmRMR)
        {
            if (_clsDashBoard.opWriteDownCurrency(prmOUID, prmName, prmRMR))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("WriteDownPocket")]
        public IActionResult PostPocket(int prmWalletOUID, int prmOUID, string prmName, double prmAEInterest, bool prmIsMain)
        {
            if (_clsDashBoard.opWriteDownPocket(prmWalletOUID, prmOUID, prmName, prmAEInterest, prmIsMain))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("WriteDownMovement")]
        public IActionResult PostMovement(int prmOUIDWallet, int prmOUIDPocket, int prmOUID, string prmName, double prmAmount, string prmDate)
        {
            if (_clsDashBoard.opWriteDownMovement(prmOUIDWallet, prmOUIDPocket, prmOUID, prmName, prmAmount, prmDate))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("UpdateWallet")]
        public IActionResult UpdateWallet(int prmOUID, string prmName, string prmClientName, string prmClientEmail)
        {
            if (_clsDashBoard.opUpdateWallet(prmOUID, prmName, prmClientName, prmClientEmail))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("UpdateCurrency")]
        public IActionResult UpdateCurrency(int prmOUID, string prmName, double prmRMR)
        {
            if (_clsDashBoard.opUpdateCurrency(prmOUID, prmName, prmRMR))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("UpdatePocket")]
        public IActionResult UpdatePocket(int prmWalletOUID, int prmOUID, string prmName, double prmAEInterest, bool prmIsMain)
        {
            if (_clsDashBoard.opUpdatePocket(prmWalletOUID, prmOUID, prmName, prmAEInterest, prmIsMain))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("DeleteWallet")]
        public IActionResult DeleteWallet(int prmOUID)
        {
            if (_clsDashBoard.opDeleteWallet(prmOUID))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("DeleteCurrency")]
        public IActionResult DeleteCurrency(int prmOUID)
        {
            if (_clsDashBoard.opDeleteCurrency(prmOUID))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("DeletePocket")]
        public IActionResult DeletePocket(int prmWalletOUID, int prmOUID)
        {
            if (_clsDashBoard.opDeletePocket(prmWalletOUID, prmOUID))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("DeleteMovement")]
        public IActionResult DeleteMovement(int prmOUIDWallet, int prmOUIDPocket, int prmOUID)
        {
            if (_clsDashBoard.opDeleteMovement(prmOUIDWallet, prmOUIDPocket, prmOUID))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
